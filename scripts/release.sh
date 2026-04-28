#!/usr/bin/env bash
# Bump version, test, pack, push to nuget.org, then commit, tag, and push to origin.
# Usage: ./scripts/release.sh <semver>
# Set NUGET_API_KEY in .env, environment, or macOS keychain (service: nuget-coinbase-prime).
# Set RELEASE_NO_CONFIRM=1 to skip "Are you sure?" prompts (e.g. CI).
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
cd "$REPO_ROOT"

# Only this project is packed and published to NuGet (not PrimeExample, generator, etc.)
CSPROJ="src/CoinbaseSdk/Prime/CoinbaseSdk.Prime.csproj"
INTEGRATION_TESTS="src/CoinbaseSdk/Prime.IntegrationTests/CoinbaseSdk.Prime.IntegrationTests.csproj"
NUGET_SOURCE="https://api.nuget.org/v3/index.json"
KEYCHAIN_SERVICE="nuget-coinbase-prime"

if [[ -z "${1-}" ]]; then
  echo "Usage: $0 <semver>  (e.g. 0.6.0 or 1.0.0-rc.1)" >&2
  exit 1
fi
VERSION="$1"

# Semver: major.minor.patch, optional -prerelease (alphanumeric, dots)
if ! [[ "$VERSION" =~ ^[0-9]+\.[0-9]+\.[0-9]+(-[0-9A-Za-z.]+)?$ ]]; then
  echo "Invalid version: $VERSION (expected e.g. 0.6.0 or 1.0.0-beta.1)" >&2
  exit 1
fi

confirm() {
  if [[ "${RELEASE_NO_CONFIRM:-}" == "1" ]]; then
    return 0
  fi
  if [[ ! -t 0 ]]; then
    echo "Not a TTY; set RELEASE_NO_CONFIRM=1 to continue." >&2
    return 1
  fi
  read -r -p "$1 [y/N] " reply
  case "$reply" in
    [yY]|[yY][eE][sS]) return 0 ;;
    *) return 1 ;;
  esac
}

# --- Load NUGET_API_KEY ---
if [[ -z "${NUGET_API_KEY:-}" && -f .env ]]; then
  while IFS= read -r line || [[ -n "$line" ]]; do
    [[ -z "$line" || "$line" =~ ^[[:space:]]*# ]] && continue
    if [[ "$line" == NUGET_API_KEY=* ]]; then
      export NUGET_API_KEY="${line#NUGET_API_KEY=}"
      # Trim optional surrounding double quotes
      NUGET_API_KEY="${NUGET_API_KEY%\"}"; NUGET_API_KEY="${NUGET_API_KEY#\"}"
      export NUGET_API_KEY
      break
    fi
  done < .env
fi
if [[ -z "${NUGET_API_KEY:-}" ]]; then
  if k="$(security find-generic-password -s "$KEYCHAIN_SERVICE" -w 2>/dev/null)"; then
    export NUGET_API_KEY="$k"
  fi
fi
if [[ -z "${NUGET_API_KEY:-}" ]]; then
  echo "NUGET_API_KEY is not set. Add it to .env, export it, or store in keychain:" >&2
  echo "  security add-generic-password -s $KEYCHAIN_SERVICE -a \$(whoami) -w <key>" >&2
  exit 1
fi

# --- Git guards ---
if ! git diff --quiet 2>/dev/null || ! git diff --cached --quiet 2>/dev/null; then
  echo "Working tree is not clean. Commit or stash changes first." >&2
  exit 1
fi
current_branch="$(git rev-parse --abbrev-ref HEAD 2>/dev/null || true)"
if [[ "$current_branch" != "main" ]]; then
  echo "Not on main (current: $current_branch). Switch to main before releasing." >&2
  exit 1
fi

# --- Date for changelog (match existing style, e.g. 2026-APR-7) ---
D=$(date +%d)
M=$(date +%b | tr '[:lower:]' '[:upper:]')
Y=$(date +%Y)
CHANGELOG_DATE="${Y}-${M}-${D}"

# --- Bump version in csproj ---
if [[ ! -f "$CSPROJ" ]]; then
  echo "Missing $CSPROJ" >&2
  exit 1
fi
sed -i '' "s|<Version>.*</Version>|<Version>${VERSION}</Version>|" "$CSPROJ"

# --- Changelog stub at top (after # Changelog) ---
CHANGELOG="CHANGELOG.md"
if [[ ! -f "$CHANGELOG" ]]; then
  echo "Missing $CHANGELOG" >&2
  exit 1
fi
TMPCL="$(mktemp)"
{
  head -1 "$CHANGELOG"
  echo ""
  {
    echo "## [${VERSION}] - ${CHANGELOG_DATE}"
    echo ""
    echo "### Added"
    echo ""
    echo "- (Add release notes before the next tag)"
  }
  echo ""
  tail -n +2 "$CHANGELOG"
} >"$TMPCL"
mv "$TMPCL" "$CHANGELOG"

# --- Build & pack (core package only) ---
# Default: restore/build/pack CoinbaseSdk.Prime only. Set RUN_INTEGRATION_TESTS=1 to also run
# integration tests against the live API (requires .env; may fail on API or permissions).
mkdir -p artifacts
dotnet restore "$CSPROJ"
if [[ "${RUN_INTEGRATION_TESTS:-}" == "1" ]]; then
  dotnet test "$INTEGRATION_TESTS" -c Release
else
  dotnet build "$CSPROJ" -c Release
fi
dotnet pack "$CSPROJ" -c Release -o ./artifacts

NUPKG="./artifacts/CoinbaseSdk.Prime.${VERSION}.nupkg"
if [[ ! -f "$NUPKG" ]]; then
  echo "Expected package not found: $NUPKG" >&2
  exit 1
fi

# --- Push to NuGet ---
if ! confirm "Push $NUPKG to nuget.org?"; then
  echo "Aborted. Revert with: git checkout -- $CSPROJ $CHANGELOG" >&2
  exit 1
fi
dotnet nuget push "$NUPKG" --api-key "$NUGET_API_KEY" --source "$NUGET_SOURCE" --skip-duplicate

# --- Commit, tag, push git ---
if ! confirm "Create commit, tag v${VERSION}, and push to origin?"; then
  echo "Package published. Commit/tag manually; working tree has version + changelog changes." >&2
  exit 0
fi
git add "$CSPROJ" "$CHANGELOG"
git commit -m "chore: release v${VERSION}"
if git rev-parse "v${VERSION}" >/dev/null 2>&1; then
  echo "Tag v${VERSION} already exists. Delete it or pick another version." >&2
  exit 1
fi
git tag "v${VERSION}"
git push
git push --tags
echo "Done. Released v${VERSION} to NuGet and pushed tag."

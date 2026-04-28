#!/usr/bin/env bash
# Build and pack to ./artifacts for local inspection (or push to a local feed).
# Optional: pass a version to set <Version> in the csproj first (revert with git checkout if only testing).
# Usage: ./scripts/pack-local.sh [semver]
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
cd "$REPO_ROOT"

# Only CoinbaseSdk.Prime is packed (not other repo projects).
CSPROJ="src/CoinbaseSdk/Prime/CoinbaseSdk.Prime.csproj"
INTEGRATION_TESTS="src/CoinbaseSdk/Prime.IntegrationTests/CoinbaseSdk.Prime.IntegrationTests.csproj"

if [[ -n "${1-}" ]]; then
  VERSION="$1"
  if ! [[ "$VERSION" =~ ^[0-9]+\.[0-9]+\.[0-9]+(-[0-9A-Za-z.]+)?$ ]]; then
    echo "Invalid version: $VERSION" >&2
    exit 1
  fi
  sed -i '' "s|<Version>.*</Version>|<Version>${VERSION}</Version>|" "$CSPROJ"
  echo "Set $CSPROJ to Version $VERSION (revert: git checkout -- $CSPROJ)"
fi

mkdir -p artifacts
dotnet restore "$CSPROJ"
if [[ "${RUN_INTEGRATION_TESTS:-}" == "1" ]]; then
  dotnet test "$INTEGRATION_TESTS" -c Release
else
  dotnet build "$CSPROJ" -c Release
fi
dotnet pack "$CSPROJ" -c Release -o ./artifacts
echo "Packed under ./artifacts/"
ls -la ./artifacts/

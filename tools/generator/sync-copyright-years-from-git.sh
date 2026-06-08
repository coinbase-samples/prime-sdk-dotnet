#!/usr/bin/env bash
# Updates the first "Copyright YYYY-present Coinbase" line in each generator source file
# to YYYY = calendar year of the commit that first added that file (git log --diff-filter=A).
# Run from repo root: bash tools/generator/sync-copyright-years-from-git.sh
# Untracked files use the current UTC year.

set -euo pipefail

ROOT=$(git rev-parse --show-toplevel)
cd "$ROOT"

sync_one() {
  local f="$1"
  [[ -f "$f" ]] || return 0
  local y
  if git ls-files --error-unmatch "$f" >/dev/null 2>&1; then
    y=$(git log --diff-filter=A --format=%cs --reverse -- "$f" 2>/dev/null | head -1 | cut -c1-4)
  else
    y=""
  fi
  if [[ -z "$y" ]]; then
    y=$(date -u +%Y)
  fi
  export GIT_YEAR="$y"
  perl -i -pe '
    if (!$done && /Copyright \d{4}-present Coinbase/) {
      s/Copyright \d{4}-present/Copyright $ENV{GIT_YEAR}-present/;
      $done = 1;
    }
  ' "$f"
}

while IFS= read -r f; do
  case "$f" in
    *.cs|*.sh|*.mustache|*.md) sync_one "$f" ;;
  esac
done < <(git ls-files 'tools/generator')

while IFS= read -r f; do
  case "$f" in
    *.cs|*.sh|*.mustache|*.md) sync_one "$f" ;;
  esac
done < <(git ls-files --others --exclude-standard 'tools/generator')

echo "Synced Copyright YYYY-present (first Git commit year) for tools/generator sources."

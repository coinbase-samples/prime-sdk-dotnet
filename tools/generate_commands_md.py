#!/usr/bin/env python3
"""Regenerate COMMANDS.md from example scripts (required CLI args + env assumptions)."""

from __future__ import annotations

import re
from collections import defaultdict
from pathlib import Path

REPO = Path(__file__).resolve().parent.parent
EXAMPLES = REPO / "src/CoinbaseSdk/PrimeExample/examples"
OUTPUT = REPO / "COMMANDS.md"

REQ_PAT = re.compile(r"Error:\s+--([a-zA-Z0-9]+)\s+is required")
EITHER_PAT = re.compile(r"Either\s+--([a-zA-Z0-9]+)\s+or\s+--([a-zA-Z0-9]+)\s+is required")

ENV_PORTFOLIO = frozenset({"portfolioId", "sourcePortfolioId"})
ENV_ENTITY = frozenset({"entityId"})


def camel_to_kebab(s: str) -> str:
    return re.sub(r"(?<!^)(?=[A-Z])", "-", s).lower()


def section_title(cat: str) -> str:
    mapping = {
        "advancedtransfer": "Advanced transfer",
        "onchainaddressbook": "On-chain address book",
    }
    return mapping.get(cat, cat.replace("_", " ").title())


def parse_file(p: Path):
    text = p.read_text(encoding="utf-8")
    m = re.search(r'new RootCommand\("([^"]*)"', text)
    title = m.group(1) if m else "?"
    required: list[str] = []
    either_groups: list[tuple[str, str]] = []
    for line in text.splitlines():
        em = EITHER_PAT.search(line)
        if em:
            either_groups.append((em.group(1), em.group(2)))
            continue
        rm = REQ_PAT.search(line)
        if rm:
            required.append(rm.group(1))
    seen: set[str] = set()
    req2: list[str] = []
    for f in required:
        if f not in seen:
            seen.add(f)
            req2.append(f)
    return title, req2, either_groups


def filter_env_flags(flags: list[str]) -> list[str]:
    return [f for f in flags if f not in ENV_PORTFOLIO and f not in ENV_ENTITY]


def main() -> None:
    files = sorted(EXAMPLES.rglob("*.cs"))
    by_cat: dict[str, list[Path]] = defaultdict(list)
    for p in files:
        cat = p.relative_to(EXAMPLES).parts[0]
        by_cat[cat].append(p)

    lines: list[str] = []
    lines.append("# Prime example commands")
    lines.append("")
    lines.append(
        "Run these from the **repository root** using "
        "[`dotnet run --file`](https://learn.microsoft.com/dotnet/core/tools/dotnet-run)."
    )
    lines.append("")
    lines.append("## Environment (required for most scripts)")
    lines.append("")
    lines.append(
        "- `PRIME_ACCESS_KEY`, `PRIME_PASSPHRASE`, `PRIME_SIGNING_KEY` — API credentials (see `.env.example`)."
    )
    lines.append(
        "- `PRIME_PORTFOLIO_ID` — used whenever an example would take `--portfolioId`, "
        "and for `--sourcePortfolioId` on allocation creates."
    )
    lines.append(
        "- `PRIME_ENTITY_ID` — used whenever an example would take `--entityId`."
    )
    lines.append("")
    lines.append(
        "This list **omits** `--portfolioId`, `--entityId`, and `--sourcePortfolioId` from the command line; "
        "set the env vars above instead. It also **omits optional** CLI flags (filters, pagination, etc.); "
        "only arguments the script treats as required are shown."
    )
    lines.append("")
    lines.append(
        "Required flags were detected from each script’s `Error: --… is required` checks "
        "(and “either/or” quantity rules for a few order examples)."
    )
    lines.append("")

    for cat in sorted(by_cat.keys()):
        lines.append(f"## {section_title(cat)}")
        lines.append("")
        for p in sorted(by_cat[cat]):
            title, req, either_groups = parse_file(p)
            req_f = filter_env_flags(req)
            rel_full = p.relative_to(REPO).as_posix()

            lines.append(f"### `{p.name}`")
            lines.append("")
            lines.append(title + ".")
            lines.append("")
            cmd = f"dotnet run --file {rel_full}"
            arg_parts: list[str] = []
            for f in req_f:
                arg_parts.append(f"--{f} <{camel_to_kebab(f)}>")
            for a, b in either_groups:
                if a in ENV_PORTFOLIO or a in ENV_ENTITY:
                    continue
                if b in ENV_PORTFOLIO or b in ENV_ENTITY:
                    continue
                arg_parts.append(f"--{a} <{camel_to_kebab(a)}>")
            if arg_parts:
                cmd += " -- " + " ".join(arg_parts)
            lines.append("```bash")
            lines.append(cmd)
            lines.append("```")
            for a, b in either_groups:
                if a in ENV_PORTFOLIO or b in ENV_PORTFOLIO or a in ENV_ENTITY or b in ENV_ENTITY:
                    continue
                lines.append("")
                lines.append(
                    f"*Use `--{b} <{camel_to_kebab(b)}>` instead of `--{a}` when sizing by quote value.*"
                )
            lines.append("")

    OUTPUT.write_text("\n".join(lines).rstrip() + "\n", encoding="utf-8")
    print(f"Wrote {OUTPUT.relative_to(REPO)}")


if __name__ == "__main__":
    main()

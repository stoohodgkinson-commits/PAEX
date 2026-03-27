# paex_cli.py
# Main entry point

import argparse
import sys
from policy_analyzer_main2 import PolicyAnalyzerMain2

def main():
    parser = argparse.ArgumentParser(description="PAEX CLI - Compare to Effective State + CSV Export")
    parser.add_argument("--baselines", nargs="+", required=True, help="One or more .PolicyRules baseline files")
    parser.add_argument("--output", "-o", required=True, help="Output CSV file")
    args = parser.parse_args()

    main2 = PolicyAnalyzerMain2()
    diagnostics = []

    success = main2.btnCompareToEffectiveState_Click(args.baselines, args.output, diagnostics)

    if diagnostics:
        print("\nDiagnostics / Warnings:")
        for d in diagnostics:
            print("  " + d)

    if success:
        print("\n=== SUCCESS ===")
        print(f"CSV written to: {args.output}")
    else:
        print("\n=== FAILED ===")
        sys.exit(1)

if __name__ == "__main__":
    main()
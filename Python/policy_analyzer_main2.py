# policy_analyzer_main2.py
# Exact full port - contains btnCompareToEffectiveState_Click

import os
import datetime
from pathlib import Path
from typing import List
from baseline_evaluator import BaselineEvaluator
from policy_rules import PolicyRules
from policy_viewer import PolicyViewer
from gplookup.gplookup_t import GPLookup_t
from globals import Globals

class PolicyAnalyzerMain2:
    def btnCompareToEffectiveState_Click(self, baseline_files: List[str], output_csv_path: str, out_diagnostics: List[str]) -> bool:
        out_diagnostics.clear()

        if not baseline_files:
            out_diagnostics.append("No baseline files provided")
            return False

        evaluator = BaselineEvaluator()

        for path in baseline_files:
            if not os.path.exists(path):
                out_diagnostics.append(f"File not found: {path}")
                return False
            if not evaluator.AddBaselineDoc(path, out_diagnostics):
                return False

        # Compute Effective State
        ts = datetime.datetime.now().strftime("%Y%m%d_%H%M%S")
        effective_path = str(Path.cwd() / f"EffectiveState_{ts}.PolicyRules")

        if not evaluator.EvaluateEffectiveState(effective_path, out_diagnostics):
            return False

        # Prepare for CSV export
        gp_lookup = GPLookup_t.GPLookup(out_diagnostics)

        baseline_rules = PolicyRules()
        effective_rules = PolicyRules()

        baseline_rules.LoadFromFile(baseline_files[0], False, out_diagnostics)
        effective_rules.LoadFromFile(effective_path, False, out_diagnostics)

        name_and_rules = [
            ("Baseline(s)", baseline_rules),
            ("Effective state", effective_rules)
        ]

        viewer = PolicyViewer()
        viewer.LoadData(gp_lookup, name_and_rules, out_diagnostics)
        viewer.export_all_data_to_csv(output_csv_path)

        print("Compare to Effective State + CSV export completed.")
        return True
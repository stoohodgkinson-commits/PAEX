# globals.py
# Exact full port of Globals.cs

import os
from pathlib import Path

class Globals:
    # ADMX path (used by GPLookup for explanation text in CSV)
    sAdmxDirectory: str = r"C:\Windows\PolicyDefinitions"

    # Default paths
    sDefaultPolicyRulesExtension: str = "PolicyRules"
    sDefaultSecurityTemplateExtension: str = "inf"
    sDefaultGpoBackupExtension: str = "backup"

    # Export options (these are the three you specifically asked to be enabled)
    bShowGpoNamesInDetailPane: bool = True
    bShowGpoNamesAndFilesInDetailPane: bool = True
    bShowExplanationText: bool = True

    # Other global settings from original C#
    bShowOnlyChangedSettings: bool = False
    bShowOnlyNonCompliantSettings: bool = False
    bIncludeEmptyGpos: bool = False

    # Last used paths (for CLI we use current directory)
    sLastOpenedBaselinePath: str = ""
    sLastOpenedGpoPath: str = ""
    sLastExportedCsvPath: str = ""

    # Registry paths used by the tool
    sPolicyAnalyzerRegKey: str = r"Software\PolicyAnalyzer"

    @staticmethod
    def GetAdmxDirectory() -> str:
        """Returns the ADMX directory used for explanation text in CSV"""
        if os.path.exists(Globals.sAdmxDirectory):
            return Globals.sAdmxDirectory
        # Fallback
        return str(Path(r"C:\Windows\PolicyDefinitions"))

    @staticmethod
    def SetAdmxDirectory(path: str):
        """Allows changing ADMX path (used for explanation text)"""
        Globals.sAdmxDirectory = path

    @staticmethod
    def GetDefaultExportFilename() -> str:
        """Default name for exported CSV"""
        return "PolicyComparison.csv"

    @staticmethod
    def GetEffectiveStateFilename() -> str:
        """Name pattern for Effective State file"""
        import datetime
        timestamp = datetime.datetime.now().strftime("%Y%m%d_%H%M%S")
        computer = os.environ.get("COMPUTERNAME", "LOCAL")
        return f"EffectiveState_{computer}_{timestamp}.PolicyRules"
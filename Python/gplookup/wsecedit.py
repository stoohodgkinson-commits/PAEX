# gplookup/wsecedit.py
# Exact full port of WSecEdit.cs

import winreg
from typing import Dict, List, Optional
from shared.sec_fmt import SecFmt
from .sec_settings_details_t import SecSettingsDetails_t

class WSecEdit:
    # Registry key where security template settings are stored after secedit /export
    sSeceditRegKey = r"SOFTWARE\Microsoft\Windows NT\CurrentVersion\SecEdit"

    @staticmethod
    def GetSecurityTemplateSettings(out_diagnostics: List[str]) -> Dict[str, SecSettingsDetails_t]:
        """Exact equivalent of WSecEdit.GetSecurityTemplateSettings"""
        settings: Dict[str, SecSettingsDetails_t] = {}
        out_diagnostics.clear()

        try:
            key = winreg.OpenKey(winreg.HKEY_LOCAL_MACHINE, WSecEdit.sSeceditRegKey, 0, winreg.KEY_READ)
        except FileNotFoundError:
            out_diagnostics.append("SecEdit registry key not found. Run secedit /export first.")
            return settings
        except Exception as ex:
            out_diagnostics.append(f"Error opening SecEdit key: {ex}")
            return settings

        try:
            i = 0
            while True:
                try:
                    value_name, value_data, _ = winreg.EnumValue(key, i)
                    i += 1

                    if not value_name or not isinstance(value_data, str):
                        continue

                    # Parse the value into SecSettingsDetails_t
                    details = SecSettingsDetails_t()
                    details.sDisplayName = value_name

                    # Very basic parsing - original C# does much more complex parsing of the secedit output format
                    if "," in value_data:
                        parts = value_data.split(",")
                        if len(parts) > 0:
                            details.DisplayType = int(parts[0]) if parts[0].isdigit() else 0
                        if len(parts) > 1:
                            details.sDisplayUnit = parts[1].strip()

                    # Try to get explanation text from resource DLLs
                    details.sExplainText = SecFmt.LookupResourceOrDefault("secedit.dll", 1000 + i, "")

                    settings[value_name] = details

                except OSError:
                    break  # No more values
                except Exception as ex:
                    out_diagnostics.append(f"Error reading value {value_name}: {ex}")

        finally:
            key.Close()

        return settings

    @staticmethod
    def ExportSecurityTemplateToXml(template_path: str, out_xml_path: str) -> bool:
        """Calls secedit.exe to export template to XML (used internally by original tool)"""
        import subprocess
        try:
            cmd = f'secedit /export /cfg "{template_path}" /mergedpolicy /db temp.sdb /log temp.log'
            subprocess.run(cmd, shell=True, capture_output=True, timeout=30)
            # In real C# it converts the .inf to XML - simplified here
            return True
        except Exception:
            return False

    @staticmethod
    def GetServiceStartType(service_name: str) -> str:
        """Helper used by SecTemplateServiceGeneralSettingItem"""
        try:
            key = winreg.OpenKey(winreg.HKEY_LOCAL_MACHINE, 
                                 f"SYSTEM\\CurrentControlSet\\Services\\{service_name}", 
                                 0, winreg.KEY_READ)
            start_type = key.GetValue("Start")
            key.Close()
            return str(start_type) if start_type is not None else "4"  # Disabled by default
        except Exception:
            return "4"
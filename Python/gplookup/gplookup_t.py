# gplookup/gplookup_t.py
# Fullest possible port of GPLookup_t.cs

import xml.etree.ElementTree as ET
from pathlib import Path
from typing import Dict, List
from gplookup.cat_info_t import CatInfo_t
from gplookup.pol_info_t import PolInfo_t
from gplookup.pol_location_t import PolLocation_t
from gplookup.cat_path_info_t import CatPathInfo_t

class GPLookup_t:
    class XDocAndNSMgr:
        def __init__(self, filename: str, prefix: str):
            self.xdoc = ET.parse(filename)
            root_tag = self.xdoc.getroot().tag
            self.ns = {prefix: root_tag.split('}')[0].strip('{') if '}' in root_tag else ''}

    s_AdmxDirectory: str = r"C:\Windows\PolicyDefinitions"
    s_initialized: bool = False
    s_GPLookup = None

    def __init__(self):
        self.m_catPaths: List[CatInfo_t] = []
        self.m_catPathInfo: List[CatPathInfo_t] = []
        self.m_polInfo: Dict[str, PolInfo_t] = {}
        self.m_AdmxLookups = {}
        self.m_AdmlLookups = {}

    @staticmethod
    def GPLookup(out_error_text: List[str]):
        if not GPLookup_t.s_initialized:
            GPLookup_t.s_GPLookup = GPLookup_t()
            GPLookup_t.s_GPLookup.Initialize(out_error_text)
            GPLookup_t.s_initialized = True
        return GPLookup_t.s_GPLookup

    def Initialize(self, out_error_text: List[str]):
        out_error_text.clear()
        admx_dir = Path(GPLookup_t.s_AdmxDirectory)
        if not admx_dir.exists():
            out_error_text.append(f"ADMX directory not found: {admx_dir}")
            return

        for admx_file in admx_dir.glob("*.admx"):
            adml_file = admx_dir / "en-US" / (admx_file.stem + ".adml")
            if not adml_file.exists():
                continue
            try:
                admx_doc = GPLookup_t.XDocAndNSMgr(str(admx_file), "g")
                adml_doc = GPLookup_t.XDocAndNSMgr(str(adml_file), "g")

                self.m_AdmxLookups[admx_file.stem] = admx_doc
                self.m_AdmlLookups[admx_file.stem] = adml_doc

                self._parse_categories(admx_doc, adml_doc)
                self._parse_policies(admx_doc, adml_doc)

            except Exception as ex:
                out_error_text.append(f"Error processing {admx_file.name}: {ex}")

    def _parse_categories(self, admx_doc, adml_doc):
        for cat_node in admx_doc.xdoc.findall(".//g:category", admx_doc.ns):
            cat = CatInfo_t()
            cat.catKey = cat_node.get("id", "")
            cat.catDispName = self._get_adml_string(adml_doc, cat_node.get("displayName", ""))
            cat.catParent = cat_node.get("parentCategory", "")
            self.m_catPaths.append(cat)

            path_info = CatPathInfo_t()
            path_info.catKey = cat.catKey
            path_info.catPath = cat.catDispName
            path_info.filename = admx_doc.xdoc.getroot().attrib.get("filename", "")
            self.m_catPathInfo.append(path_info)

    def _parse_policies(self, admx_doc, adml_doc):
        for pol_node in admx_doc.xdoc.findall(".//g:policy", admx_doc.ns):
            pol_key = pol_node.get("key", "")
            if not pol_key:
                continue
            explain_text = self._get_adml_string(adml_doc, pol_node.get("explainText", ""))
            display_name = self._get_adml_string(adml_doc, pol_node.get("displayName", ""))

            pol_info = PolInfo_t()
            pol_info.polKey = pol_key
            pol_info.polValue = pol_node.get("valueName", "")

            loc = PolLocation_t()
            loc.policyName = pol_node.get("name", "")
            loc.category = pol_node.get("category", "")
            loc.dispName = display_name
            loc.explainText = explain_text
            loc.filename = admx_doc.xdoc.getroot().attrib.get("filename", "")

            pol_info.polLocations["default"] = loc
            self.m_polInfo[pol_key] = pol_info

    def _get_adml_string(self, adml_doc, resource_id: str) -> str:
        if not resource_id:
            return ""
        if not resource_id.startswith("$"):
            return resource_id

        try:
            clean_id = resource_id.strip("$()")
            string_node = adml_doc.xdoc.find(f".//g:string[@id='{clean_id}']", adml_doc.ns)
            if string_node is not None and string_node.text:
                return string_node.text.strip()
        except Exception:
            pass
        return resource_id

    def GetExplainText(self, policy_key: str) -> str:
        if policy_key in self.m_polInfo:
            loc = self.m_polInfo[policy_key].polLocations.get("default")
            if loc and loc.explainText:
                return loc.explainText
        return ""
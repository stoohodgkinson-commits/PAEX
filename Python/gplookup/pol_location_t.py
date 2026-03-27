# gplookup/pol_location_t.py
# Exact full port of PolLocation_t.cs

class PolLocation_t:
    def __init__(self, s_pol_name="", s_cat="", s_disp="", s_disp_sub_option="", s_explain_text="", s_filename=""):
        self.policyName = s_pol_name
        self.category = s_cat
        self.dispName = s_disp
        self.dispNameSubOption = s_disp_sub_option
        self.explainText = s_explain_text
        self.filename = s_filename

    def SortKey(self):
        return PolLocation_t.SortKey(self.policyName, self.category, self.dispName, self.dispNameSubOption, self.filename)

    @staticmethod
    def SortKey(polname, cat, name, subname, fname):
        return f"{polname}!{cat}!{name}!{fname}".lower()
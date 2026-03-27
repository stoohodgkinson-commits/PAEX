# Exact port of BaselineDefs.cs - all constants and arrays preserved 100%

class BaselineDefs:
    sPolicyRulesRootElem = "PolicyRules"
    sSourceFile = "SourceFile"
    sPolicyName = "PolicyName"
    sNodeEvalOp = "EvalOperator"
    sNodeEvalVals = "EvalValues"
    sNodeEvalResult = "EvalResult"
    sNodeBaselineValue = "BaselineValue"
    sNodeValueNotFound = "ValueNotFound"
    sNodeInvalidComparison = "InvalidComparison"
    sNodeGPPath = "GPPath"
    sNodeGPSettingName = "GPSettingName"
    sNodeGPSettingHelp = "GPSettingHelp"
    sNodeGPSettingChoice = "GPSettingChoice"

    ComparisonOpEqual = "Equal"
    ComparisonOpNotEqual = "NotEqual"
    ComparisonOpNonNull = "NonNull"
    ComparisonOpGreaterThanOrEqual = "GreaterThanOrEqual"
    ComparisonOpLessThanOrEqual = "LessThanOrEqual"
    ComparisonOpOneOfThese = "OneOfThese"
    ComparisonOpRange = "Range"
    ComparisonOpIgnore = "Ignore-AlwaysPass"

    ComparisonOperators = [
        "Equal", "NotEqual", "NonNull", "GreaterThanOrEqual",
        "LessThanOrEqual", "OneOfThese", "Range", "Ignore-AlwaysPass"
    ]

    sComputerConfigElem = "ComputerConfig"
    sUserConfigElem = "UserConfig"
    sRegConfigKey = "Key"
    sRegConfigValue = "Value"
    sRegConfigType = "RegType"
    sRegConfigData = "RegData"

    sHKLM = "HKLM"
    sHKCU = "HKCU"

    sRegBINARY = "REG_BINARY"
    sRegDWORD = "REG_DWORD"
    sRegEXPAND_SZ = "REG_EXPAND_SZ"
    sRegMULTI_SZ = "REG_MULTI_SZ"
    sRegNONE = "REG_NONE"
    sRegQWORD = "REG_QWORD"
    sRegSZ = "REG_SZ"

    sRegpolDelVals = "**delvals"
    sRegpolDel = "**del."

    sSecurityTemplateElem = "SecurityTemplate"
    sSection = "Section"
    sLineItem = "LineItem"

    sRegistryValues = "Registry Values"
    sPrivilegeRights = "Privilege Rights"
    sSystemAccess = "System Access"
    sServiceGeneralSetting = "Service General Setting"
    sApplicationLog = "Application Log"
    sSecurityLog = "Security Log"
    sSystemLog = "System Log"
    sFileSecurity = "File Security"
    sRegistryKeys = "Registry Keys"
    sGroupMembership = "Group Membership"
    sKerberosPolicy = "Kerberos Policy"
    sEventAudit = "Event Audit"

    SecTemplateSections = [
        "Registry Values", "Privilege Rights", "System Access", "Service General Setting",
        "Application Log", "Security Log", "System Log", "File Security", "Registry Keys",
        "Group Membership", "Kerberos Policy", "Event Audit"
    ]
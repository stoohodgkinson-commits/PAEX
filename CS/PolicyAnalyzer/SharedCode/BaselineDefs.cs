using System;

namespace SharedCode
{
	// Token: 0x02000038 RID: 56
	internal class BaselineDefs
	{
		// Token: 0x04000616 RID: 1558
		public const string sPolicyRulesRootElem = "PolicyRules";

		// Token: 0x04000617 RID: 1559
		public const string sSourceFile = "SourceFile";

		// Token: 0x04000618 RID: 1560
		public const string sPolicyName = "PolicyName";

		// Token: 0x04000619 RID: 1561
		public const string sNodeEvalOp = "EvalOperator";

		// Token: 0x0400061A RID: 1562
		public const string sNodeEvalVals = "EvalValues";

		// Token: 0x0400061B RID: 1563
		public const string sNodeEvalResult = "EvalResult";

		// Token: 0x0400061C RID: 1564
		public const string sNodeBaselineValue = "BaselineValue";

		// Token: 0x0400061D RID: 1565
		public const string sNodeValueNotFound = "ValueNotFound";

		// Token: 0x0400061E RID: 1566
		public const string sNodeInvalidComparison = "InvalidComparison";

		// Token: 0x0400061F RID: 1567
		public const string sNodeGPPath = "GPPath";

		// Token: 0x04000620 RID: 1568
		public const string sNodeGPSettingName = "GPSettingName";

		// Token: 0x04000621 RID: 1569
		public const string sNodeGPSettingHelp = "GPSettingHelp";

		// Token: 0x04000622 RID: 1570
		public const string sNodeGPSettingChoice = "GPSettingChoice";

		// Token: 0x04000623 RID: 1571
		public const string ComparisonOpEqual = "Equal";

		// Token: 0x04000624 RID: 1572
		public const string ComparisonOpNotEqual = "NotEqual";

		// Token: 0x04000625 RID: 1573
		public const string ComparisonOpNonNull = "NonNull";

		// Token: 0x04000626 RID: 1574
		public const string ComparisonOpGreaterThanOrEqual = "GreaterThanOrEqual";

		// Token: 0x04000627 RID: 1575
		public const string ComparisonOpLessThanOrEqual = "LessThanOrEqual";

		// Token: 0x04000628 RID: 1576
		public const string ComparisonOpOneOfThese = "OneOfThese";

		// Token: 0x04000629 RID: 1577
		public const string ComparisonOpRange = "Range";

		// Token: 0x0400062A RID: 1578
		public const string ComparisonOpIgnore = "Ignore-AlwaysPass";

		// Token: 0x0400062B RID: 1579
		public static string[] ComparisonOperators = new string[] { "Equal", "NotEqual", "NonNull", "GreaterThanOrEqual", "LessThanOrEqual", "OneOfThese", "Range", "Ignore-AlwaysPass" };

		// Token: 0x0400062C RID: 1580
		public const string sComputerConfigElem = "ComputerConfig";

		// Token: 0x0400062D RID: 1581
		public const string sUserConfigElem = "UserConfig";

		// Token: 0x0400062E RID: 1582
		public const string sRegConfigKey = "Key";

		// Token: 0x0400062F RID: 1583
		public const string sRegConfigValue = "Value";

		// Token: 0x04000630 RID: 1584
		public const string sRegConfigType = "RegType";

		// Token: 0x04000631 RID: 1585
		public const string sRegConfigData = "RegData";

		// Token: 0x04000632 RID: 1586
		public const string sHKLM = "HKLM";

		// Token: 0x04000633 RID: 1587
		public const string sHKCU = "HKCU";

		// Token: 0x04000634 RID: 1588
		public const string sRegBINARY = "REG_BINARY";

		// Token: 0x04000635 RID: 1589
		public const string sRegDWORD = "REG_DWORD";

		// Token: 0x04000636 RID: 1590
		public const string sRegEXPAND_SZ = "REG_EXPAND_SZ";

		// Token: 0x04000637 RID: 1591
		public const string sRegMULTI_SZ = "REG_MULTI_SZ";

		// Token: 0x04000638 RID: 1592
		public const string sRegNONE = "REG_NONE";

		// Token: 0x04000639 RID: 1593
		public const string sRegQWORD = "REG_QWORD";

		// Token: 0x0400063A RID: 1594
		public const string sRegSZ = "REG_SZ";

		// Token: 0x0400063B RID: 1595
		public const string sRegpolDelVals = "**delvals";

		// Token: 0x0400063C RID: 1596
		public const string sRegpolDel = "**del.";

		// Token: 0x0400063D RID: 1597
		public const string sSecurityTemplateElem = "SecurityTemplate";

		// Token: 0x0400063E RID: 1598
		public const string sSection = "Section";

		// Token: 0x0400063F RID: 1599
		public const string sLineItem = "LineItem";

		// Token: 0x04000640 RID: 1600
		public const string sRegistryValues = "Registry Values";

		// Token: 0x04000641 RID: 1601
		public const string sPrivilegeRights = "Privilege Rights";

		// Token: 0x04000642 RID: 1602
		public const string sSystemAccess = "System Access";

		// Token: 0x04000643 RID: 1603
		public const string sServiceGeneralSetting = "Service General Setting";

		// Token: 0x04000644 RID: 1604
		public const string sApplicationLog = "Application Log";

		// Token: 0x04000645 RID: 1605
		public const string sSecurityLog = "Security Log";

		// Token: 0x04000646 RID: 1606
		public const string sSystemLog = "System Log";

		// Token: 0x04000647 RID: 1607
		public const string sFileSecurity = "File Security";

		// Token: 0x04000648 RID: 1608
		public const string sRegistryKeys = "Registry Keys";

		// Token: 0x04000649 RID: 1609
		public const string sGroupMembership = "Group Membership";

		// Token: 0x0400064A RID: 1610
		public const string sKerberosPolicy = "Kerberos Policy";

		// Token: 0x0400064B RID: 1611
		public const string sEventAudit = "Event Audit";

		// Token: 0x0400064C RID: 1612
		public static string[] SecTemplateSections = new string[]
		{
			"Registry Values", "Privilege Rights", "System Access", "Service General Setting", "Application Log", "Security Log", "System Log", "File Security", "Registry Keys", "Group Membership",
			"Kerberos Policy", "Event Audit"
		};

		// Token: 0x0400064D RID: 1613
		public const string SecTemplateRegValuesXPath = "//SecurityTemplate[@Section='Registry Values']";

		// Token: 0x0400064E RID: 1614
		public const string SecTemplateNonRegValuesXPath = "//SecurityTemplate[@Section!='Registry Values']";

		// Token: 0x0400064F RID: 1615
		public const string sAuditSubcatElem = "AuditSubcategory";

		// Token: 0x04000650 RID: 1616
		public const string sAuditGUID = "GUID";

		// Token: 0x04000651 RID: 1617
		public const string sAuditName = "Name";

		// Token: 0x04000652 RID: 1618
		public const string sAuditSetting = "Setting";

		// Token: 0x04000653 RID: 1619
		public const string sGlobalAuditElem = "GlobalAudit";

		// Token: 0x04000654 RID: 1620
		public const string sGlobalAuditType = "Type";

		// Token: 0x04000655 RID: 1621
		public const string sGlobalAuditSACL = "SACL";

		// Token: 0x04000656 RID: 1622
		public const string sGlobalAuditTypeFile = "FileGlobalSacl";

		// Token: 0x04000657 RID: 1623
		public const string sGlobalAuditTypeReg = "RegistryGlobalSacl";

		// Token: 0x04000658 RID: 1624
		public string[] GlobalAuditTypeValues = new string[] { "FileGlobalSacl", "RegistryGlobalSacl" };

		// Token: 0x04000659 RID: 1625
		public const string sAuditOptionElem = "AuditOption";

		// Token: 0x0400065A RID: 1626
		public const string sAuditOptionOption = "Option";

		// Token: 0x0400065B RID: 1627
		public const string sAuditOptionSetting = "Setting";

		// Token: 0x0400065C RID: 1628
		public const string sAudOptAuditBaseDirectories = "Option:AuditBaseDirectories";

		// Token: 0x0400065D RID: 1629
		public const string sAudOptAuditBaseObjects = "Option:AuditBaseObjects";

		// Token: 0x0400065E RID: 1630
		public const string sAudOptCrashOnAuditFail = "Option:CrashOnAuditFail";

		// Token: 0x0400065F RID: 1631
		public const string sAudOptFullPrivilegeAuditing = "Option:FullPrivilegeAuditing";

		// Token: 0x04000660 RID: 1632
		public string[] AuditOptionOptionValues = new string[] { "Option:AuditBaseDirectories", "Option:AuditBaseObjects", "Option:CrashOnAuditFail", "Option:FullPrivilegeAuditing" };

		// Token: 0x04000661 RID: 1633
		public const string sCSEMachineElem = "CSE-Machine";

		// Token: 0x04000662 RID: 1634
		public const string sCSEUserElem = "CSE-User";

		// Token: 0x04000663 RID: 1635
		public const string sCSEGuid = "GUID";

		// Token: 0x04000664 RID: 1636
		public const string sCSEName = "Name";
	}
}

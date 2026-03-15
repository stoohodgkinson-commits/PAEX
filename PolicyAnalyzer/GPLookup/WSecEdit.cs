using System;
using System.Collections.Generic;
using SharedCode;

namespace GPLookup
{
	// Token: 0x02000037 RID: 55
	public class WSecEdit
	{
		// Token: 0x0600009A RID: 154 RVA: 0x0000847C File Offset: 0x0000667C
		public static bool HelpIdFromResourceId(uint resourceId, out uint helpId)
		{
			helpId = 0U;
			if (WSecEdit.ResourceToHelpIdMap == null)
			{
				WSecEdit.ResourceToHelpIdMap = new Dictionary<uint, uint>();
				for (int i = 0; i < WSecEdit.nResourceIDtoExplainText.GetLength(0); i++)
				{
					WSecEdit.ResourceToHelpIdMap.Add(WSecEdit.nResourceIDtoExplainText[i, 0], WSecEdit.nResourceIDtoExplainText[i, 1]);
				}
			}
			return WSecEdit.ResourceToHelpIdMap.TryGetValue(resourceId, out helpId);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000084E4 File Offset: 0x000066E4
		public static bool HelpIdFromRegkeyName(string sKeyname, out uint helpId)
		{
			helpId = 0U;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(sKeyname);
			if (num <= 2224039223U)
			{
				if (num <= 621224119U)
				{
					if (num <= 296350494U)
					{
						if (num != 209553757U)
						{
							if (num == 296350494U)
							{
								if (sKeyname == "MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\EnableInstallerDetection")
								{
									helpId = 2051U;
									return true;
								}
							}
						}
						else if (sKeyname == "MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\ValidateAdminCodeSignatures")
						{
							helpId = 2052U;
							return true;
						}
					}
					else if (num != 484127676U)
					{
						if (num == 621224119U)
						{
							if (sKeyname == "MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\ConsentPromptBehaviorAdmin")
							{
								helpId = 2049U;
								return true;
							}
						}
					}
					else if (sKeyname == "MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\EnableVirtualization")
					{
						helpId = 2056U;
						return true;
					}
				}
				else if (num <= 1205833465U)
				{
					if (num != 1180981904U)
					{
						if (num == 1205833465U)
						{
							if (sKeyname == "MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\EnableLUA")
							{
								helpId = 2054U;
								return true;
							}
						}
					}
					else if (sKeyname == "MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\PromptOnSecureDesktop")
					{
						helpId = 2055U;
						return true;
					}
				}
				else if (num != 1536410264U)
				{
					if (num == 2224039223U)
					{
						if (sKeyname == "MACHINE\\Software\\Policies\\Microsoft\\Windows NT\\DCOM\\MachineLaunchRestriction")
						{
							helpId = 2048U;
							return true;
						}
					}
				}
				else if (sKeyname == "MACHINE\\System\\CurrentControlSet\\Services\\LanManServer\\Parameters\\EnableS4U2SelfForClaims")
				{
					helpId = 2076U;
					return true;
				}
			}
			else if (num <= 3053579970U)
			{
				if (num <= 2336778579U)
				{
					if (num != 2286247973U)
					{
						if (num == 2336778579U)
						{
							if (sKeyname == "MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\ConsentPromptBehaviorUser")
							{
								helpId = 2050U;
								return true;
							}
						}
					}
					else if (sKeyname == "MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\InactivityTimeoutSecs")
					{
						helpId = 2079U;
						return true;
					}
				}
				else if (num != 2846919574U)
				{
					if (num == 3053579970U)
					{
						if (sKeyname == "MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\FilterAdministratorToken")
						{
							helpId = 2046U;
							return true;
						}
					}
				}
				else if (sKeyname == "MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\EnableUIADesktopToggle")
				{
					helpId = 2059U;
					return true;
				}
			}
			else if (num <= 3390513135U)
			{
				if (num != 3142768324U)
				{
					if (num == 3390513135U)
					{
						if (sKeyname == "MACHINE\\System\\CurrentControlSet\\Services\\LanManServer\\Parameters\\SmbServerNameHardeningLevel")
						{
							helpId = 2075U;
							return true;
						}
					}
				}
				else if (sKeyname == "MACHINE\\Software\\Policies\\Microsoft\\Windows NT\\DCOM\\MachineAccessRestriction")
				{
					helpId = 2047U;
					return true;
				}
			}
			else if (num != 3913416699U)
			{
				if (num == 3975333204U)
				{
					if (sKeyname == "MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\MaxDevicePasswordFailedAttempts")
					{
						helpId = 2078U;
						return true;
					}
				}
			}
			else if (sKeyname == "MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\EnableSecureUIAPaths")
			{
				helpId = 2053U;
				return true;
			}
			return false;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00002BFA File Offset: 0x00000DFA
		public static string Lookup(uint id)
		{
			return new DllResourceStringLookup().LookupResource("wsecedit.dll", id);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000087F4 File Offset: 0x000069F4
		public static string LookupWithDefault(uint id, string sDefault)
		{
			string text = WSecEdit.Lookup(id);
			if (text.Length > 0)
			{
				return text;
			}
			return sDefault;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00008814 File Offset: 0x00006A14
		public static string LookupUpToLineBreak(uint id, string sDefault)
		{
			string text = WSecEdit.Lookup(id);
			if (text.Length <= 0)
			{
				return sDefault;
			}
			int num = text.IndexOf('\n');
			if (num > 0)
			{
				return text.Substring(0, num);
			}
			return text;
		}

		// Token: 0x04000140 RID: 320
		public const int IDS_EXTENSION_DESC = 1;

		// Token: 0x04000141 RID: 321
		public const int IDS_NAME = 2;

		// Token: 0x04000142 RID: 322
		public const int IDS_DESC = 3;

		// Token: 0x04000143 RID: 323
		public const int IDS_ATTR = 4;

		// Token: 0x04000144 RID: 324
		public const int IDS_BASE_ANALYSIS = 5;

		// Token: 0x04000145 RID: 325
		public const int IDS_SETTING = 6;

		// Token: 0x04000146 RID: 326
		public const int IDS_NODENAME = 7;

		// Token: 0x04000147 RID: 327
		public const int IDS_CONFIGURE = 8;

		// Token: 0x04000148 RID: 328
		public const int IDS_ANALYZE = 9;

		// Token: 0x04000149 RID: 329
		public const int IDS_CONFIGURE_DESC = 10;

		// Token: 0x0400014A RID: 330
		public const int IDS_ANALYZE_DESC = 11;

		// Token: 0x0400014B RID: 331
		public const int IDS_POLICY = 12;

		// Token: 0x0400014C RID: 332
		public const int IDS_PRIVILEGE = 13;

		// Token: 0x0400014D RID: 333
		public const int IDS_GROUPS = 14;

		// Token: 0x0400014E RID: 334
		public const int IDS_SERVICE = 15;

		// Token: 0x0400014F RID: 335
		public const int IDS_REGISTRY = 16;

		// Token: 0x04000150 RID: 336
		public const int IDS_FILESTORE = 17;

		// Token: 0x04000151 RID: 337
		public const int IDS_PRIVILEGE_DESC = 19;

		// Token: 0x04000152 RID: 338
		public const int IDS_GROUPS_DESC = 20;

		// Token: 0x04000153 RID: 339
		public const int IDS_SERVICE_DESC = 21;

		// Token: 0x04000154 RID: 340
		public const int IDS_REGISTRY_DESC = 22;

		// Token: 0x04000155 RID: 341
		public const int IDS_FILESTORE_DESC = 23;

		// Token: 0x04000156 RID: 342
		public const int IDS_TEMPLATE_EDITOR_NAME = 24;

		// Token: 0x04000157 RID: 343
		public const int IDS_NOT_SAVED_SUFFIX = 25;

		// Token: 0x04000158 RID: 344
		public const int IDS_EXTENSION_NAME = 26;

		// Token: 0x04000159 RID: 345
		public const int IDS_SCE_DESC = 27;

		// Token: 0x0400015A RID: 346
		public const int IDS_SAV_DESC = 28;

		// Token: 0x0400015B RID: 347
		public const int IDS_DELETE_CONFIRM = 29;

		// Token: 0x0400015C RID: 348
		public const int IDS_ABOUT_SECMGR = 30;

		// Token: 0x0400015D RID: 349
		public const int IDS_DELETE_ALL_ITEMS = 30;

		// Token: 0x0400015E RID: 350
		public const int IDS_ANALYZE_PROFILE = 31;

		// Token: 0x0400015F RID: 351
		public const int IDS_GENERATE_PROFILE = 34;

		// Token: 0x04000160 RID: 352
		public const int IDS_ADD_FILES = 35;

		// Token: 0x04000161 RID: 353
		public const int IDS_ADD_LOCATION = 36;

		// Token: 0x04000162 RID: 354
		public const int IDS_NEW_PROFILE = 37;

		// Token: 0x04000163 RID: 355
		public const int IDS_REMOVE_LOCATION = 38;

		// Token: 0x04000164 RID: 356
		public const int IDS_RELOAD_LOCATION = 39;

		// Token: 0x04000165 RID: 357
		public const int IDS_APPLY_PROFILE = 40;

		// Token: 0x04000166 RID: 358
		public const int IDS_IMPORT_POLICY_FAIL = 42;

		// Token: 0x04000167 RID: 359
		public const int IDS_SAVEAS_PROFILE = 43;

		// Token: 0x04000168 RID: 360
		public const int IDS_COPY_PROFILE = 44;

		// Token: 0x04000169 RID: 361
		public const int IDS_PASTE_PROFILE = 45;

		// Token: 0x0400016A RID: 362
		public const int IDS_RSOP_GPO = 46;

		// Token: 0x0400016B RID: 363
		public const int IDS_REFRESH_AREA = 47;

		// Token: 0x0400016C RID: 364
		public const int IDS_SECURITY_REQUIRED = 48;

		// Token: 0x0400016D RID: 365
		public const int IDS_CANT_ASSIGN_SECURITY = 49;

		// Token: 0x0400016E RID: 366
		public const int IDS_PASSWORD_CATEGORY = 50;

		// Token: 0x0400016F RID: 367
		public const int IDS_MAX_PAS_AGE = 51;

		// Token: 0x04000170 RID: 368
		public const int IDS_MIN_PAS_AGE = 52;

		// Token: 0x04000171 RID: 369
		public const int IDS_MIN_PAS_LEN = 53;

		// Token: 0x04000172 RID: 370
		public const int IDS_PAS_UNIQUENESS = 54;

		// Token: 0x04000173 RID: 371
		public const int IDS_PAS_COMPLEX = 55;

		// Token: 0x04000174 RID: 372
		public const int IDS_REQ_LOGON = 56;

		// Token: 0x04000175 RID: 373
		public const int IDS_LOCKOUT_CATEGORY = 57;

		// Token: 0x04000176 RID: 374
		public const int IDS_LOCK_COUNT = 58;

		// Token: 0x04000177 RID: 375
		public const int IDS_LOCK_RESET_COUNT = 59;

		// Token: 0x04000178 RID: 376
		public const int IDS_LOCK_DURATION = 60;

		// Token: 0x04000179 RID: 377
		public const int IDS_CONFIRM_DELETE_TEMPLATE = 61;

		// Token: 0x0400017A RID: 378
		public const int IDS_ANALYZE_NT4 = 62;

		// Token: 0x0400017B RID: 379
		public const int IDS_FORCE_LOGOFF = 63;

		// Token: 0x0400017C RID: 380
		public const int IDS_NEW_ADMIN = 65;

		// Token: 0x0400017D RID: 381
		public const int IDS_NEW_GUEST = 67;

		// Token: 0x0400017E RID: 382
		public const int IDS_REFRESH_LOCALPOL = 68;

		// Token: 0x0400017F RID: 383
		public const int IDS_GROUP_NAME = 69;

		// Token: 0x04000180 RID: 384
		public const int IDS_EVENT_LOG = 70;

		// Token: 0x04000181 RID: 385
		public const int IDS_SYS_LOG_MAX = 71;

		// Token: 0x04000182 RID: 386
		public const int IDS_SYS_LOG_RET = 72;

		// Token: 0x04000183 RID: 387
		public const int IDS_SYS_LOG_DAYS = 73;

		// Token: 0x04000184 RID: 388
		public const int IDS_SEC_LOG_MAX = 74;

		// Token: 0x04000185 RID: 389
		public const int IDS_SEC_LOG_RET = 75;

		// Token: 0x04000186 RID: 390
		public const int IDS_SEC_LOG_DAYS = 76;

		// Token: 0x04000187 RID: 391
		public const int IDS_APP_LOG_MAX = 77;

		// Token: 0x04000188 RID: 392
		public const int IDS_APP_LOG_RET = 78;

		// Token: 0x04000189 RID: 393
		public const int IDS_APP_LOG_DAYS = 79;

		// Token: 0x0400018A RID: 394
		public const int IDS_CRASH_LOG_FULL = 80;

		// Token: 0x0400018B RID: 395
		public const int IDS_EVENT_AUDIT = 81;

		// Token: 0x0400018C RID: 396
		public const int IDS_EVENT_ON = 82;

		// Token: 0x0400018D RID: 397
		public const int IDS_SYSTEM_EVENT = 83;

		// Token: 0x0400018E RID: 398
		public const int IDS_LOGON_EVENT = 84;

		// Token: 0x0400018F RID: 399
		public const int IDS_OBJECT_ACCESS = 85;

		// Token: 0x04000190 RID: 400
		public const int IDS_PRIVILEGE_USE = 86;

		// Token: 0x04000191 RID: 401
		public const int IDS_POLICY_CHANGE = 87;

		// Token: 0x04000192 RID: 402
		public const int IDS_ACCOUNT_MANAGE = 88;

		// Token: 0x04000193 RID: 403
		public const int IDS_PROCESS_TRACK = 89;

		// Token: 0x04000194 RID: 404
		public const int IDS_OTHER_CATEGORY = 90;

		// Token: 0x04000195 RID: 405
		public const int IDS_NO_POLICY = 92;

		// Token: 0x04000196 RID: 406
		public const int IDS_CANT_ADD_MEMBER = 95;

		// Token: 0x04000197 RID: 407
		public const int IDS_CANT_SHOW_SECURITY = 96;

		// Token: 0x04000198 RID: 408
		public const int IDS_CANT_ADD_USER = 97;

		// Token: 0x04000199 RID: 409
		public const int IDS_CANT_ADD_DSOBJECT = 98;

		// Token: 0x0400019A RID: 410
		public const int IDS_CANT_ADD_FOLDER = 99;

		// Token: 0x0400019B RID: 411
		public const int IDS_GRP_MEMBERS = 100;

		// Token: 0x0400019C RID: 412
		public const int IDS_GRP_MEMBEROF = 101;

		// Token: 0x0400019D RID: 413
		public const int IDS_NO_DBCS = 102;

		// Token: 0x0400019E RID: 414
		public const int IDS_PERMISSION = 103;

		// Token: 0x0400019F RID: 415
		public const int IDS_AUDITING = 104;

		// Token: 0x040001A0 RID: 416
		public const int IDD_CONFIG_PRIVS = 106;

		// Token: 0x040001A1 RID: 417
		public const int IDS_CANT_ADD_FILE = 106;

		// Token: 0x040001A2 RID: 418
		public const int IDD_CONFIG_MEMBERSHIP = 107;

		// Token: 0x040001A3 RID: 419
		public const int IDS_INVALID_NAME = 107;

		// Token: 0x040001A4 RID: 420
		public const int IDS_ERROR_GENERATE = 108;

		// Token: 0x040001A5 RID: 421
		public const int IDS_CANT_ADD_KEY = 109;

		// Token: 0x040001A6 RID: 422
		public const int IDS_FILE_GEN_ALL = 110;

		// Token: 0x040001A7 RID: 423
		public const int IDS_FILE_GEN_MODIFY = 111;

		// Token: 0x040001A8 RID: 424
		public const int IDS_FILE_GEN_READ = 112;

		// Token: 0x040001A9 RID: 425
		public const int IDS_FILE_GEN_LIST = 113;

		// Token: 0x040001AA RID: 426
		public const int IDS_FILE_SPEC_READ = 114;

		// Token: 0x040001AB RID: 427
		public const int IDS_FILE_SPEC_WRITE = 115;

		// Token: 0x040001AC RID: 428
		public const int IDS_FILE_SPEC_EXECUTE = 116;

		// Token: 0x040001AD RID: 429
		public const int IDS_FILE_SPEC_DELETE = 117;

		// Token: 0x040001AE RID: 430
		public const int IDS_NONE = 120;

		// Token: 0x040001AF RID: 431
		public const int IDS_KEY_ALL_ACCESS = 121;

		// Token: 0x040001B0 RID: 432
		public const int IDS_KEY_READ = 122;

		// Token: 0x040001B1 RID: 433
		public const int IDS_KEY_WRITE = 123;

		// Token: 0x040001B2 RID: 434
		public const int IDS_KEY_QUERY_VALUE = 124;

		// Token: 0x040001B3 RID: 435
		public const int IDS_KEY_SET_VALUE = 125;

		// Token: 0x040001B4 RID: 436
		public const int IDS_KEY_CREATE_SUB_KEY = 126;

		// Token: 0x040001B5 RID: 437
		public const int IDS_KEY_ENUMERATE_SUB_KEYS = 127;

		// Token: 0x040001B6 RID: 438
		public const int IDS_KEY_NOTIFY = 128;

		// Token: 0x040001B7 RID: 439
		public const int IDS_KEY_CREATE_LINK = 129;

		// Token: 0x040001B8 RID: 440
		public const int IDS_KEY_EXECUTE = 130;

		// Token: 0x040001B9 RID: 441
		public const int IDS_CANT_CREATE_THREAD = 131;

		// Token: 0x040001BA RID: 442
		public const int IDS_ERR_INVALIDACCOUNT = 132;

		// Token: 0x040001BB RID: 443
		public const int IDS_STATIC_LOG_NAME = 141;

		// Token: 0x040001BC RID: 444
		public const int IDS_ANALYZE_TITLE = 143;

		// Token: 0x040001BD RID: 445
		public const int IDS_SECURITY_SETTING_DESC = 144;

		// Token: 0x040001BE RID: 446
		public const int IDS_SECMGR_VERSION = 146;

		// Token: 0x040001BF RID: 447
		public const int IDS_SECMGR_DESC = 147;

		// Token: 0x040001C0 RID: 448
		public const int IDS_LAST_ANALYSIS = 148;

		// Token: 0x040001C1 RID: 449
		public const int IDS_PROFILE_DESC = 149;

		// Token: 0x040001C2 RID: 450
		public const int IDS_NO_ANALYSIS = 150;

		// Token: 0x040001C3 RID: 451
		public const int IDS_NEVER_ANALYSIS = 151;

		// Token: 0x040001C4 RID: 452
		public const int IDS_TITLE_ABOUT_SECMGR = 152;

		// Token: 0x040001C5 RID: 453
		public const int IDS_TITLE_ABOUT_ANALYSIS = 153;

		// Token: 0x040001C6 RID: 454
		public const int IDS_ADDLOCATION_TITLE = 154;

		// Token: 0x040001C7 RID: 455
		public const int IDS_COL_OBJECT = 165;

		// Token: 0x040001C8 RID: 456
		public const int IDS_COL_BAD_COUNT = 166;

		// Token: 0x040001C9 RID: 457
		public const int IDD_ATTR_STRING = 167;

		// Token: 0x040001CA RID: 458
		public const int IDD_ATTR_NUMBER = 168;

		// Token: 0x040001CB RID: 459
		public const int IDS_OPEN_PROFILE = 168;

		// Token: 0x040001CC RID: 460
		public const int IDD_ATTR_ENABLE = 169;

		// Token: 0x040001CD RID: 461
		public const int IDS_PROFILE_FILTER = 169;

		// Token: 0x040001CE RID: 462
		public const int IDD_ATTR_AUDIT = 170;

		// Token: 0x040001CF RID: 463
		public const int IDS_PROFILE_DEF_EXT = 170;

		// Token: 0x040001D0 RID: 464
		public const int IDS_OPEN_LOGFILE = 171;

		// Token: 0x040001D1 RID: 465
		public const int IDS_LOGFILE_DEF_EXT = 172;

		// Token: 0x040001D2 RID: 466
		public const int IDD_ANALYZE_SECURITY = 173;

		// Token: 0x040001D3 RID: 467
		public const int IDS_LOGFILE_FILTER = 173;

		// Token: 0x040001D4 RID: 468
		public const int IDD_REGISTRY_DIALOG = 177;

		// Token: 0x040001D5 RID: 469
		public const int IDD_CONFIG_AUDIT = 180;

		// Token: 0x040001D6 RID: 470
		public const int IDD_CONFIG_NUMBER = 181;

		// Token: 0x040001D7 RID: 471
		public const int IDD_CONFIG_ENABLE = 182;

		// Token: 0x040001D8 RID: 472
		public const int IDD_CONFIG_NAME = 183;

		// Token: 0x040001D9 RID: 473
		public const int IDD_SAVE_TEMPLATES = 186;

		// Token: 0x040001DA RID: 474
		public const int IDD_ATTR_RET = 189;

		// Token: 0x040001DB RID: 475
		public const int IDD_CONFIG_RET = 190;

		// Token: 0x040001DC RID: 476
		public const int IDD_ATTR_RIGHT = 191;

		// Token: 0x040001DD RID: 477
		public const int IDS_TEMPLATE_LOCATION_KEY = 193;

		// Token: 0x040001DE RID: 478
		public const int IDD_ANALYSIS_GENERATE = 193;

		// Token: 0x040001DF RID: 479
		public const int IDS_ERROR_CANT_OPEN_PROFILE = 194;

		// Token: 0x040001E0 RID: 480
		public const int IDD_ANALYSIS_SERVICE = 194;

		// Token: 0x040001E1 RID: 481
		public const int IDS_ERROR_CANT_GET_PROFILE_INFO = 195;

		// Token: 0x040001E2 RID: 482
		public const int IDD_CONFIG_SERVICE = 195;

		// Token: 0x040001E3 RID: 483
		public const int IDS_ERROR_NO_ANALYSIS_INFO = 196;

		// Token: 0x040001E4 RID: 484
		public const int IDS_FOREVER = 197;

		// Token: 0x040001E5 RID: 485
		public const int IDD_CONFIG_OBJECT = 197;

		// Token: 0x040001E6 RID: 486
		public const int IDS_AS_NEEDED = 198;

		// Token: 0x040001E7 RID: 487
		public const int IDD_ATTR_OBJECT = 198;

		// Token: 0x040001E8 RID: 488
		public const int IDI_SCE = 199;

		// Token: 0x040001E9 RID: 489
		public const int IDS_BY_DAYS = 199;

		// Token: 0x040001EA RID: 490
		public const int IDD_ANALYZE_PROGRESS = 199;

		// Token: 0x040001EB RID: 491
		public const int IDI_SCE_APP = 199;

		// Token: 0x040001EC RID: 492
		public const int IDS_MANUALLY = 200;

		// Token: 0x040001ED RID: 493
		public const int IDS_ENABLED = 201;

		// Token: 0x040001EE RID: 494
		public const int IDC_STATIC_DESC = 202;

		// Token: 0x040001EF RID: 495
		public const int IDS_DISABLED = 202;

		// Token: 0x040001F0 RID: 496
		public const int IDC_STATIC_TITLE = 203;

		// Token: 0x040001F1 RID: 497
		public const int IDS_ON = 203;

		// Token: 0x040001F2 RID: 498
		public const int IDC_PROGRESS = 204;

		// Token: 0x040001F3 RID: 499
		public const int IDS_OFF = 204;

		// Token: 0x040001F4 RID: 500
		public const int IDS_NOT_CONFIGURED = 205;

		// Token: 0x040001F5 RID: 501
		public const int IDB_ICON16 = 205;

		// Token: 0x040001F6 RID: 502
		public const int IDD_ABOUT_SECMGR = 209;

		// Token: 0x040001F7 RID: 503
		public const int IDD_APPLY = 210;

		// Token: 0x040001F8 RID: 504
		public const int IDS_COL_MEMBERSHIP = 210;

		// Token: 0x040001F9 RID: 505
		public const int IDS_COL_MEMBEROF = 211;

		// Token: 0x040001FA RID: 506
		public const int IDB_ICON32 = 211;

		// Token: 0x040001FB RID: 507
		public const int IDD_APPLY_CONFIGURATION = 212;

		// Token: 0x040001FC RID: 508
		public const int IDS_HKCR = 214;

		// Token: 0x040001FD RID: 509
		public const int IDD_ATTR_GROUP = 214;

		// Token: 0x040001FE RID: 510
		public const int IDC_DESCRIPTION = 215;

		// Token: 0x040001FF RID: 511
		public const int IDS_HKLM = 215;

		// Token: 0x04000200 RID: 512
		public const int IDD_PERFORM_ANALYSIS = 215;

		// Token: 0x04000201 RID: 513
		public const int IDS_HKU = 216;

		// Token: 0x04000202 RID: 514
		public const int IDD_ASSIGN_PROFILE = 216;

		// Token: 0x04000203 RID: 515
		public const int IDS_SCESTATUS_SUCCESS = 217;

		// Token: 0x04000204 RID: 516
		public const int IDD_ASSIGN_CONFIG_CHECK = 217;

		// Token: 0x04000205 RID: 517
		public const int IDD_SET_DESCRIPTION = 218;

		// Token: 0x04000206 RID: 518
		public const int IDB_ARROW = 220;

		// Token: 0x04000207 RID: 519
		public const int IDB_CHECK = 221;

		// Token: 0x04000208 RID: 520
		public const int IDC_DATABASE_DIR = 223;

		// Token: 0x04000209 RID: 521
		public const int IDB_SCE_LARGE = 223;

		// Token: 0x0400020A RID: 522
		public const int IDC_CHANGE_PROFILE = 224;

		// Token: 0x0400020B RID: 523
		public const int IDB_SCE_SMALL = 224;

		// Token: 0x0400020C RID: 524
		public const int IDC_LAST_ANALYSIS_TIME = 225;

		// Token: 0x0400020D RID: 525
		public const int IDD_NEW_PROFILE = 225;

		// Token: 0x0400020E RID: 526
		public const int IDC_PROFILE = 226;

		// Token: 0x0400020F RID: 527
		public const int IDD_LOCALPOL_AUDIT = 226;

		// Token: 0x04000210 RID: 528
		public const int IDC_ERROR_LOG = 227;

		// Token: 0x04000211 RID: 529
		public const int IDC_NC_POLICY = 227;

		// Token: 0x04000212 RID: 530
		public const int IDD_LOCALPOL_ENABLE = 227;

		// Token: 0x04000213 RID: 531
		public const int IDC_CHANGE_DATABASE_DIR = 228;

		// Token: 0x04000214 RID: 532
		public const int IDC_TITLE = 228;

		// Token: 0x04000215 RID: 533
		public const int IDC_NC_GROUPS = 228;

		// Token: 0x04000216 RID: 534
		public const int IDD_LOCALPOL_NUMBER = 228;

		// Token: 0x04000217 RID: 535
		public const int IDC_CHANGE_ERROR_LOG = 229;

		// Token: 0x04000218 RID: 536
		public const int IDC_CURRENT = 229;

		// Token: 0x04000219 RID: 537
		public const int IDC_NC_PRIVELEGES = 229;

		// Token: 0x0400021A RID: 538
		public const int IDC_TITLE_MEMBER_OF = 229;

		// Token: 0x0400021B RID: 539
		public const int IDS_UNKNOWN_ERROR = 229;

		// Token: 0x0400021C RID: 540
		public const int IDD_LOCALPOL_RET = 229;

		// Token: 0x0400021D RID: 541
		public const int IDC_NC_REGISTRY = 230;

		// Token: 0x0400021E RID: 542
		public const int IDD_LOCALPOL_STRING = 230;

		// Token: 0x0400021F RID: 543
		public const int IDC_NC_FILESYSTEM = 231;

		// Token: 0x04000220 RID: 544
		public const int IDD_LOCALPOL_RIGHT = 231;

		// Token: 0x04000221 RID: 545
		public const int IDS_DEFAULT_TEMPLATE_DIR = 232;

		// Token: 0x04000222 RID: 546
		public const int IDC_LAST_CONFIG_TIME = 232;

		// Token: 0x04000223 RID: 547
		public const int IDC_SPIN = 233;

		// Token: 0x04000224 RID: 548
		public const int IDS_SE_NETWORK_LOGON_RIGHT = 233;

		// Token: 0x04000225 RID: 549
		public const int IDC_NC_SERVICE = 233;

		// Token: 0x04000226 RID: 550
		public const int IDC_NEW = 234;

		// Token: 0x04000227 RID: 551
		public const int IDS_SE_INTERACTIVE_LOGON_RIGHT = 234;

		// Token: 0x04000228 RID: 552
		public const int IDC_UNITS = 235;

		// Token: 0x04000229 RID: 553
		public const int IDS_SAVE_FAILED = 235;

		// Token: 0x0400022A RID: 554
		public const int IDD_LOCALPOL_REGCHOICES = 235;

		// Token: 0x0400022B RID: 555
		public const int IDC_IGNORE = 236;

		// Token: 0x0400022C RID: 556
		public const int IDS_SAVE_PROFILE = 236;

		// Token: 0x0400022D RID: 557
		public const int IDD_CONFIG_REGCHOICES = 236;

		// Token: 0x0400022E RID: 558
		public const int IDC_ENABLED = 237;

		// Token: 0x0400022F RID: 559
		public const int IDS_SECEDIT_KEY = 237;

		// Token: 0x04000230 RID: 560
		public const int IDD_ATTR_REGCHOICES = 237;

		// Token: 0x04000231 RID: 561
		public const int IDD_WARNING_DIALOG = 238;

		// Token: 0x04000232 RID: 562
		public const int IDC_DISABLED = 238;

		// Token: 0x04000233 RID: 563
		public const int IDS_ADDFOLDER_TITLE = 238;

		// Token: 0x04000234 RID: 564
		public const int IDS_ADD_FOLDER = 239;

		// Token: 0x04000235 RID: 565
		public const int IDD_DOMAIN_AUDIT = 239;

		// Token: 0x04000236 RID: 566
		public const int IDS_ADD_KEY = 240;

		// Token: 0x04000237 RID: 567
		public const int IDD_DOMAIN_ENABLE = 240;

		// Token: 0x04000238 RID: 568
		public const int IDC_CHANGE = 241;

		// Token: 0x04000239 RID: 569
		public const int IDS_ADD_GROUP = 241;

		// Token: 0x0400023A RID: 570
		public const int IDD_DOMAIN_MEMBERSHIP = 241;

		// Token: 0x0400023B RID: 571
		public const int IDC_ACCEPT = 242;

		// Token: 0x0400023C RID: 572
		public const int IDB_TEMPLATE = 242;

		// Token: 0x0400023D RID: 573
		public const int IDD_DOMAIN_NAME = 242;

		// Token: 0x0400023E RID: 574
		public const int IDC_CHANGE_SUCCESS = 243;

		// Token: 0x0400023F RID: 575
		public const int IDB_ANALYSIS = 243;

		// Token: 0x04000240 RID: 576
		public const int IDD_DOMAIN_NUMBER = 243;

		// Token: 0x04000241 RID: 577
		public const int IDC_CHANGE_FAILED = 244;

		// Token: 0x04000242 RID: 578
		public const int IDC_CURRENT_MEMBER_OF = 244;

		// Token: 0x04000243 RID: 579
		public const int IDI_TEMPLATE = 244;

		// Token: 0x04000244 RID: 580
		public const int IDD_DOMAIN_OBJECT = 244;

		// Token: 0x04000245 RID: 581
		public const int IDC_RECC_MEMBER_OF = 245;

		// Token: 0x04000246 RID: 582
		public const int IDI_LOCALPOLICY = 245;

		// Token: 0x04000247 RID: 583
		public const int IDD_DOMAIN_PRIVS = 245;

		// Token: 0x04000248 RID: 584
		public const int IDC_ADD = 246;

		// Token: 0x04000249 RID: 585
		public const int IDI_POLICY = 246;

		// Token: 0x0400024A RID: 586
		public const int IDD_DOMAIN_REGCHOICES = 246;

		// Token: 0x0400024B RID: 587
		public const int IDC_REMOVE = 247;

		// Token: 0x0400024C RID: 588
		public const int IDI_ANALYSIS = 247;

		// Token: 0x0400024D RID: 589
		public const int IDD_DOMAIN_RET = 247;

		// Token: 0x0400024E RID: 590
		public const int IDC_ADD_MEMBEROF = 248;

		// Token: 0x0400024F RID: 591
		public const int IDS_COL_SERVICE = 248;

		// Token: 0x04000250 RID: 592
		public const int IDB_POLICY = 248;

		// Token: 0x04000251 RID: 593
		public const int IDI_WARNING_SMALL = 248;

		// Token: 0x04000252 RID: 594
		public const int IDD_DOMAIN_SERVICE = 248;

		// Token: 0x04000253 RID: 595
		public const int IDC_REMOVE_MEMBEROF = 249;

		// Token: 0x04000254 RID: 596
		public const int IDS_STARTUP = 249;

		// Token: 0x04000255 RID: 597
		public const int IDB_LOCALPOLICY = 249;

		// Token: 0x04000256 RID: 598
		public const int IDC_ACCEPT_CURRENT = 250;

		// Token: 0x04000257 RID: 599
		public const int IDS_INSPECTED = 250;

		// Token: 0x04000258 RID: 600
		public const int IDS_CONFIGURED = 251;

		// Token: 0x04000259 RID: 601
		public const int IDS_AUTOMATIC = 252;

		// Token: 0x0400025A RID: 602
		public const int IDC_RECC_PRIV = 253;

		// Token: 0x0400025B RID: 603
		public const int IDS_MANUAL = 253;

		// Token: 0x0400025C RID: 604
		public const int IDD_CONFIG_REGMULTISZ = 253;

		// Token: 0x0400025D RID: 605
		public const int IDC_CURRENT_PRIV = 254;

		// Token: 0x0400025E RID: 606
		public const int IDS_OK = 254;

		// Token: 0x0400025F RID: 607
		public const int IDD_ATTR_REGMULTISZ = 254;

		// Token: 0x04000260 RID: 608
		public const int IDS_INVESTIGATE = 255;

		// Token: 0x04000261 RID: 609
		public const int IDD_LOCALPOL_REGMULTISZ = 255;

		// Token: 0x04000262 RID: 610
		public const int IDS_CONFIG_SECURITY_PAGE = 256;

		// Token: 0x04000263 RID: 611
		public const int IDD_DOMAIN_REGMULTISZ = 256;

		// Token: 0x04000264 RID: 612
		public const int IDC_APPLY = 257;

		// Token: 0x04000265 RID: 613
		public const int IDS_ANALYSIS_SECURITY_PAGE = 257;

		// Token: 0x04000266 RID: 614
		public const int IDD_CONFIG_REGFLAGS = 257;

		// Token: 0x04000267 RID: 615
		public const int IDS_SECURITY_PAGE = 258;

		// Token: 0x04000268 RID: 616
		public const int IDD_ATTR_REGFLAGS = 258;

		// Token: 0x04000269 RID: 617
		public const int IDS_SERVICE_ALL = 259;

		// Token: 0x0400026A RID: 618
		public const int IDD_LOCALPOL_REGFLAGS = 259;

		// Token: 0x0400026B RID: 619
		public const int IDC_VIEW = 260;

		// Token: 0x0400026C RID: 620
		public const int IDS_SERVICE_READ = 260;

		// Token: 0x0400026D RID: 621
		public const int IDD_DOMAIN_REGFLAGS = 260;

		// Token: 0x0400026E RID: 622
		public const int IDD_ADD_OBJECT = 261;

		// Token: 0x0400026F RID: 623
		public const int IDC_VIEW_EDIT = 261;

		// Token: 0x04000270 RID: 624
		public const int IDS_SERVICE_WRITE = 261;

		// Token: 0x04000271 RID: 625
		public const int IDS_GROUP_MEMBER_OF_HEADER = 262;

		// Token: 0x04000272 RID: 626
		public const int IDD_PRECEDENCE = 262;

		// Token: 0x04000273 RID: 627
		public const int IDS_GROUP_MEMBERS_HEADER = 263;

		// Token: 0x04000274 RID: 628
		public const int IDD_REDZONE_WARNING = 233;

		// Token: 0x04000275 RID: 629
		public const int IDD_DCOM_CONFIG_NAME = 263;

		// Token: 0x04000276 RID: 630
		public const int IDD_DCOM_LOCALPOL_STRING = 264;

		// Token: 0x04000277 RID: 631
		public const int IDD_DCOM_DOMAIN_NAME = 265;

		// Token: 0x04000278 RID: 632
		public const int IDD_DCOM_ATTR_STRING = 266;

		// Token: 0x04000279 RID: 633
		public const int IDS_GROUP_MEMBER_OF_PAGE_TITLE = 264;

		// Token: 0x0400027A RID: 634
		public const int IDC_CURRENT_MEMBERS = 265;

		// Token: 0x0400027B RID: 635
		public const int IDS_GROUP_MEMBERS_PAGE_TITLE = 265;

		// Token: 0x0400027C RID: 636
		public const int IDC_RECC_MEMBERS = 266;

		// Token: 0x0400027D RID: 637
		public const int IDS_ACCOUNT_POLICY = 266;

		// Token: 0x0400027E RID: 638
		public const int IDS_ACCOUNT_DESC = 267;

		// Token: 0x0400027F RID: 639
		public const int IDS_LOCAL_POLICY = 268;

		// Token: 0x04000280 RID: 640
		public const int IDS_LOCAL_DESC = 269;

		// Token: 0x04000281 RID: 641
		public const int IDS_EVENTLOG_POLICY = 270;

		// Token: 0x04000282 RID: 642
		public const int IDC_SUCCESSFUL = 271;

		// Token: 0x04000283 RID: 643
		public const int IDS_EVENTLOG_DESC = 271;

		// Token: 0x04000284 RID: 644
		public const int IDC_FAILED = 272;

		// Token: 0x04000285 RID: 645
		public const int IDS_DIRECTORY_ACCESS = 272;

		// Token: 0x04000286 RID: 646
		public const int IDS_ACCOUNT_LOGON = 273;

		// Token: 0x04000287 RID: 647
		public const int IDC_VALUE = 275;

		// Token: 0x04000288 RID: 648
		public const int IDS_SYS_LOG_GUEST = 275;

		// Token: 0x04000289 RID: 649
		public const int IDS_SEC_LOG_GUEST = 276;

		// Token: 0x0400028A RID: 650
		public const int IDS_APP_LOG_GUEST = 277;

		// Token: 0x0400028B RID: 651
		public const int IDS_LM_FULL = 278;

		// Token: 0x0400028C RID: 652
		public const int IDS_OBJECT_IGNORE = 281;

		// Token: 0x0400028D RID: 653
		public const int IDC_NAME = 283;

		// Token: 0x0400028E RID: 654
		public const int IDS_OBJECT_OVERWRITE = 284;

		// Token: 0x0400028F RID: 655
		public const int IDS_SE_BATCH_LOGON_RIGHT = 286;

		// Token: 0x04000290 RID: 656
		public const int IDC_GRANTLIST = 287;

		// Token: 0x04000291 RID: 657
		public const int IDS_SE_SERVICE_LOGON_RIGHT = 287;

		// Token: 0x04000292 RID: 658
		public const int IDC_MEMBEROF = 288;

		// Token: 0x04000293 RID: 659
		public const int IDS_DSOBJECT = 288;

		// Token: 0x04000294 RID: 660
		public const int IDS_DSOBJECT_DESC = 289;

		// Token: 0x04000295 RID: 661
		public const int IDC_MEMBERS = 290;

		// Token: 0x04000296 RID: 662
		public const int IDS_ADD_DSOBJECT = 290;

		// Token: 0x04000297 RID: 663
		public const int IDC_TITLE_MEMBERS = 291;

		// Token: 0x04000298 RID: 664
		public const int IDS_FILE_GENERIC_READ = 291;

		// Token: 0x04000299 RID: 665
		public const int IDC_ADD_MEMBER = 292;

		// Token: 0x0400029A RID: 666
		public const int IDS_FILE_GENERIC_WRITE = 292;

		// Token: 0x0400029B RID: 667
		public const int IDC_REMOVE_MEMBER = 293;

		// Token: 0x0400029C RID: 668
		public const int IDS_FILE_SPEC_READ_DATA = 293;

		// Token: 0x0400029D RID: 669
		public const int IDC_REGTREE = 294;

		// Token: 0x0400029E RID: 670
		public const int IDS_FILE_SPEC_READ_ATTR = 294;

		// Token: 0x0400029F RID: 671
		public const int IDC_TEMPLATE_LIST = 295;

		// Token: 0x040002A0 RID: 672
		public const int IDS_FILE_SPEC_READ_EA = 295;

		// Token: 0x040002A1 RID: 673
		public const int IDS_FILE_SPEC_WRITE_DATA = 296;

		// Token: 0x040002A2 RID: 674
		public const int IDS_FILE_SPEC_APPEND_DATA = 297;

		// Token: 0x040002A3 RID: 675
		public const int IDS_FILE_SPEC_WRITE_ATTR = 298;

		// Token: 0x040002A4 RID: 676
		public const int IDS_FILE_SPEC_WRITE_EA = 299;

		// Token: 0x040002A5 RID: 677
		public const int IDS_FILE_SPEC_DELETE_CHILD = 300;

		// Token: 0x040002A6 RID: 678
		public const int IDC_LAST_INSPECT = 301;

		// Token: 0x040002A7 RID: 679
		public const int IDS_STD_DELETE = 301;

		// Token: 0x040002A8 RID: 680
		public const int IDC_RETENTION = 302;

		// Token: 0x040002A9 RID: 681
		public const int IDS_STD_READ_CONTROL = 302;

		// Token: 0x040002AA RID: 682
		public const int IDC_ATTRIBUTE_NAME = 303;

		// Token: 0x040002AB RID: 683
		public const int IDS_STD_WRITE_DAC = 303;

		// Token: 0x040002AC RID: 684
		public const int IDC_CONFIGURE = 304;

		// Token: 0x040002AD RID: 685
		public const int IDS_STD_WRITE_OWNER = 304;

		// Token: 0x040002AE RID: 686
		public const int IDC_RIGHTS = 305;

		// Token: 0x040002AF RID: 687
		public const int IDS_STD_SYNCHRONIZE = 305;

		// Token: 0x040002B0 RID: 688
		public const int IDC_STATIC_FILENAME = 306;

		// Token: 0x040002B1 RID: 689
		public const int IDS_FILE_GENERAL_PUBLISH = 306;

		// Token: 0x040002B2 RID: 690
		public const int IDC_BASESD = 307;

		// Token: 0x040002B3 RID: 691
		public const int IDC_OVERWRITE = 307;

		// Token: 0x040002B4 RID: 692
		public const int IDS_FILE_GENERAL_DEPOSIT = 307;

		// Token: 0x040002B5 RID: 693
		public const int IDC_CURRENTSD = 308;

		// Token: 0x040002B6 RID: 694
		public const int IDC_RADIO2 = 308;

		// Token: 0x040002B7 RID: 695
		public const int IDS_FILE_GENERIC_EXECUTE = 308;

		// Token: 0x040002B8 RID: 696
		public const int IDC_RADIO3 = 309;

		// Token: 0x040002B9 RID: 697
		public const int IDS_FILE_FOLDER = 309;

		// Token: 0x040002BA RID: 698
		public const int IDS_FILE_FOLDER_SUBITEMS = 310;

		// Token: 0x040002BB RID: 699
		public const int IDC_SECURITY = 311;

		// Token: 0x040002BC RID: 700
		public const int IDS_FILE_FOLDER_SUBFOLDER = 311;

		// Token: 0x040002BD RID: 701
		public const int IDS_FILE_FOLDER_FILE = 312;

		// Token: 0x040002BE RID: 702
		public const int IDC_TEMPLATE_SECURITY = 313;

		// Token: 0x040002BF RID: 703
		public const int IDS_FILE_SUBITEMS_ONLY = 313;

		// Token: 0x040002C0 RID: 704
		public const int IDC_INSPECTED_SECURITY = 314;

		// Token: 0x040002C1 RID: 705
		public const int IDS_FILE_SUBFOLDER_ONLY = 314;

		// Token: 0x040002C2 RID: 706
		public const int IDC_INSPECTED = 315;

		// Token: 0x040002C3 RID: 707
		public const int IDS_FILE_FILE_ONLY = 315;

		// Token: 0x040002C4 RID: 708
		public const int IDC_HEADER = 316;

		// Token: 0x040002C5 RID: 709
		public const int IDS_KEY_FOLDER = 316;

		// Token: 0x040002C6 RID: 710
		public const int IDS_KEY_FOLDER_SUBFOLDER = 317;

		// Token: 0x040002C7 RID: 711
		public const int IDC_AUTOINHERIT = 318;

		// Token: 0x040002C8 RID: 712
		public const int IDS_KEY_SUBFOLDER_ONLY = 318;

		// Token: 0x040002C9 RID: 713
		public const int IDC_NOAUTOINHERIT = 319;

		// Token: 0x040002CA RID: 714
		public const int IDS_KEY_FOLDER_SUBITEMS = 319;

		// Token: 0x040002CB RID: 715
		public const int IDS_KEY_SUBITEMS_ONLY = 320;

		// Token: 0x040002CC RID: 716
		public const int IDC_CHECK = 320;

		// Token: 0x040002CD RID: 717
		public const int IDS_SERVICE_EXECUTE = 321;

		// Token: 0x040002CE RID: 718
		public const int IDC_PROGRESS1 = 321;

		// Token: 0x040002CF RID: 719
		public const int IDS_SERVICE_QUERY_CONFIG = 322;

		// Token: 0x040002D0 RID: 720
		public const int IDC_ICON1 = 322;

		// Token: 0x040002D1 RID: 721
		public const int IDS_SERVICE_CHANGE_CONFIG = 323;

		// Token: 0x040002D2 RID: 722
		public const int IDC_ICON2 = 323;

		// Token: 0x040002D3 RID: 723
		public const int IDS_SERVICE_QUERY_STATUS = 324;

		// Token: 0x040002D4 RID: 724
		public const int IDC_ICON3 = 324;

		// Token: 0x040002D5 RID: 725
		public const int IDS_SERVICE_ENUMERATE = 325;

		// Token: 0x040002D6 RID: 726
		public const int IDC_ICON4 = 325;

		// Token: 0x040002D7 RID: 727
		public const int IDS_SERVICE_START = 326;

		// Token: 0x040002D8 RID: 728
		public const int IDC_ICON5 = 326;

		// Token: 0x040002D9 RID: 729
		public const int IDS_SERVICE_STOP = 327;

		// Token: 0x040002DA RID: 730
		public const int IDC_ICON6 = 327;

		// Token: 0x040002DB RID: 731
		public const int IDS_SERVICE_PAUSE = 328;

		// Token: 0x040002DC RID: 732
		public const int IDC_ICON7 = 328;

		// Token: 0x040002DD RID: 733
		public const int IDS_SERVICE_INTERROGATE = 329;

		// Token: 0x040002DE RID: 734
		public const int IDS_SERVICE_USER_CONTROL = 330;

		// Token: 0x040002DF RID: 735
		public const int IDC_AREA1 = 330;

		// Token: 0x040002E0 RID: 736
		public const int IDS_DS_ALL = 331;

		// Token: 0x040002E1 RID: 737
		public const int IDC_AREA2 = 331;

		// Token: 0x040002E2 RID: 738
		public const int IDC_LI_TITLE = 331;

		// Token: 0x040002E3 RID: 739
		public const int IDS_DS_READ = 332;

		// Token: 0x040002E4 RID: 740
		public const int IDC_AREA3 = 332;

		// Token: 0x040002E5 RID: 741
		public const int IDC_TEMPLATE_TITLE = 332;

		// Token: 0x040002E6 RID: 742
		public const int IDS_DS_WRITE = 333;

		// Token: 0x040002E7 RID: 743
		public const int IDC_AREA4 = 333;

		// Token: 0x040002E8 RID: 744
		public const int IDC_CHECK1 = 333;

		// Token: 0x040002E9 RID: 745
		public const int IDC_AREA5 = 334;

		// Token: 0x040002EA RID: 746
		public const int IDC_INCREMENTAL = 334;

		// Token: 0x040002EB RID: 747
		public const int IDS_DS_ACTRL_CREATE = 335;

		// Token: 0x040002EC RID: 748
		public const int IDC_AREA6 = 335;

		// Token: 0x040002ED RID: 749
		public const int IDC_LOG_FILE = 335;

		// Token: 0x040002EE RID: 750
		public const int IDS_DS_ACTRL_DELETE = 336;

		// Token: 0x040002EF RID: 751
		public const int IDC_AREA7 = 336;

		// Token: 0x040002F0 RID: 752
		public const int IDC_BROWSE = 336;

		// Token: 0x040002F1 RID: 753
		public const int IDS_DS_ACTRL_LIST = 337;

		// Token: 0x040002F2 RID: 754
		public const int IDS_DS_ACTRL_SELF = 338;

		// Token: 0x040002F3 RID: 755
		public const int IDC_ERROR = 338;

		// Token: 0x040002F4 RID: 756
		public const int IDC_REGKEY = 339;

		// Token: 0x040002F5 RID: 757
		public const int IDS_DS_ACTRL_READ_PROP = 339;

		// Token: 0x040002F6 RID: 758
		public const int IDS_DS_ACTRL_WRITE_PROP = 340;

		// Token: 0x040002F7 RID: 759
		public const int IDC_VERB = 340;

		// Token: 0x040002F8 RID: 760
		public const int IDS_DS_FOLDER = 341;

		// Token: 0x040002F9 RID: 761
		public const int IDC_CONFIG_NAME = 341;

		// Token: 0x040002FA RID: 762
		public const int IDS_DS_FOLDER_SUBFOLDER = 342;

		// Token: 0x040002FB RID: 763
		public const int IDC_EFFECTIVE_POLICY = 342;

		// Token: 0x040002FC RID: 764
		public const int IDS_DS_SUBFOLDER_ONLY = 343;

		// Token: 0x040002FD RID: 765
		public const int IDC_CHOICES = 343;

		// Token: 0x040002FE RID: 766
		public const int IDS_COMPUTER_TEMPLATE = 344;

		// Token: 0x040002FF RID: 767
		public const int IDC_FAILEDLIST = 344;

		// Token: 0x04000300 RID: 768
		public const int IDS_NO_LOCKOUT = 345;

		// Token: 0x04000301 RID: 769
		public const int IDC_WARNING = 345;

		// Token: 0x04000302 RID: 770
		public const int IDS_CHANGE_IMMEDIATELY = 346;

		// Token: 0x04000303 RID: 771
		public const int IDS_PERMIT_BLANK = 347;

		// Token: 0x04000304 RID: 772
		public const int IDS_NO_HISTORY = 348;

		// Token: 0x04000305 RID: 773
		public const int IDS_PASSWORD_EXPIRE = 349;

		// Token: 0x04000306 RID: 774
		public const int IDC_ANALYZED_COMPUTER_STRING_STATIC = 349;

		// Token: 0x04000307 RID: 775
		public const int IDS_PASSWORD_CHANGE = 350;

		// Token: 0x04000308 RID: 776
		public const int IDC_CHANGE_TEMPLATE_SETTING_STATIC = 350;

		// Token: 0x04000309 RID: 777
		public const int IDS_PASSWORD_LEN = 351;

		// Token: 0x0400030A RID: 778
		public const int IDC_ANALYZED_COMPUTER_SETTING_STATIC = 351;

		// Token: 0x0400030B RID: 779
		public const int IDS_PASSWORD_REMEMBER = 352;

		// Token: 0x0400030C RID: 780
		public const int IDC_REGISTRY_STATIC = 352;

		// Token: 0x0400030D RID: 781
		public const int IDS_LOCKOUT_AFTER = 353;

		// Token: 0x0400030E RID: 782
		public const int IDC_SELECT_TO_SAVE_STATIC = 353;

		// Token: 0x0400030F RID: 783
		public const int IDS_DURATION = 354;

		// Token: 0x04000310 RID: 784
		public const int IDC_LAST_INSPECTED_STATICSTATIC = 354;

		// Token: 0x04000311 RID: 785
		public const int IDS_OVERWRITE_EVENT = 355;

		// Token: 0x04000312 RID: 786
		public const int IDC_ASSIGNED_TO_STATIC = 355;

		// Token: 0x04000313 RID: 787
		public const int IDS_PASSWORD_FOREVER = 356;

		// Token: 0x04000314 RID: 788
		public const int IDC_CONFIGURATION_SETTING_STATIC = 356;

		// Token: 0x04000315 RID: 789
		public const int IDS_LOCKOUT_FOREVER = 357;

		// Token: 0x04000316 RID: 790
		public const int IDC_ANALYZED_SETTING_STATIC = 357;

		// Token: 0x04000317 RID: 791
		public const int IDC_SERVICE_STARTUP_MODE_STATIC = 358;

		// Token: 0x04000318 RID: 792
		public const int IDS_CLEAR_PASSWORD = 359;

		// Token: 0x04000319 RID: 793
		public const int IDC_TEMPLATE_SETTING_STATIC = 359;

		// Token: 0x0400031A RID: 794
		public const int IDS_KERBEROS_CATEGORY = 360;

		// Token: 0x0400031B RID: 795
		public const int IDC_ERROR_LOG_PROFILE_PATH_STATIC = 360;

		// Token: 0x0400031C RID: 796
		public const int IDS_KERBEROS_VALIDATE_CLIENT = 361;

		// Token: 0x0400031D RID: 797
		public const int IDC_DESCRIPTION_STATIC = 361;

		// Token: 0x0400031E RID: 798
		public const int IDS_KERBEROS_MAX_CLOCK = 362;

		// Token: 0x0400031F RID: 799
		public const int IDC_TEMPLATE_NAME_STATIC = 362;

		// Token: 0x04000320 RID: 800
		public const int IDS_KERBEROS_MAX_SERVICE = 363;

		// Token: 0x04000321 RID: 801
		public const int IDC_EFFECTIVE_POLICY_SETTING_STATIC = 363;

		// Token: 0x04000322 RID: 802
		public const int IDS_KERBEROS_MAX_AGE = 364;

		// Token: 0x04000323 RID: 803
		public const int IDC_CHANGE_LOCAL_POLICY_TO_STATIC = 364;

		// Token: 0x04000324 RID: 804
		public const int IDS_KERBEROS_RENEWAL = 365;

		// Token: 0x04000325 RID: 805
		public const int IDC_LOCAL_POLICY_STATIC = 365;

		// Token: 0x04000326 RID: 806
		public const int IDS_ADDMEMBER = 366;

		// Token: 0x04000327 RID: 807
		public const int IDC_EFFECTIVE_POLICY_STATIC = 366;

		// Token: 0x04000328 RID: 808
		public const int IDS_CANT_DISPLAY_ERROR_OOM = 368;

		// Token: 0x04000329 RID: 809
		public const int IDC_SELECTED_KEY = 368;

		// Token: 0x0400032A RID: 810
		public const int IDS_ERROR_GETTING_LAST_ANALYSIS = 369;

		// Token: 0x0400032B RID: 811
		public const int IDC_DEFINE_GROUP = 369;

		// Token: 0x0400032C RID: 812
		public const int IDS_ERROR_CANT_SAVE = 370;

		// Token: 0x0400032D RID: 813
		public const int IDC_STATIC_DESCRIPTION = 370;

		// Token: 0x0400032E RID: 814
		public const int IDS_QUERY_DELETE = 371;

		// Token: 0x0400032F RID: 815
		public const int IDS_ANALYSIS_VIEWER_NAME = 372;

		// Token: 0x04000330 RID: 816
		public const int IDC_STATIC_LABEL = 372;

		// Token: 0x04000331 RID: 817
		public const int IDC_STATIC_DESCRIPTION2 = 372;

		// Token: 0x04000332 RID: 818
		public const int IDS_MISMATCH = 373;

		// Token: 0x04000333 RID: 819
		public const int IDS_ASSIGN_CONFIGURATION = 374;

		// Token: 0x04000334 RID: 820
		public const int IDS_CANT_OPEN_LOG_FILE = 376;

		// Token: 0x04000335 RID: 821
		public const int IDS_LOGFILE_PICKER_TITLE = 377;

		// Token: 0x04000336 RID: 822
		public const int IDS_DEFAULT_DB_EXTENSION = 378;

		// Token: 0x04000337 RID: 823
		public const int IDS_DB_FILTER = 379;

		// Token: 0x04000338 RID: 824
		public const int IDS_APPLY_CONFIGURATION = 380;

		// Token: 0x04000339 RID: 825
		public const int IDS_LOG_FILE_FOR_APPLY = 381;

		// Token: 0x0400033A RID: 826
		public const int IDC_SETTING = 381;

		// Token: 0x0400033B RID: 827
		public const int IDS_SECURITY_MENU_ITEM = 382;

		// Token: 0x0400033C RID: 828
		public const int IDC_CONFIG = 382;

		// Token: 0x0400033D RID: 829
		public const int IDS_SECURITY_MENU_ITEM_DESC = 383;

		// Token: 0x0400033E RID: 830
		public const int IDC_PREVENT = 383;

		// Token: 0x0400033F RID: 831
		public const int IDS_CONFIGURATION_KEY = 384;

		// Token: 0x04000340 RID: 832
		public const int IDC_INHERIT = 384;

		// Token: 0x04000341 RID: 833
		public const int IDS_SECURITY_MENU = 385;

		// Token: 0x04000342 RID: 834
		public const int IDS_ENV_VARS_REG_VALUE = 386;

		// Token: 0x04000343 RID: 835
		public const int IDC_NO_MEMBERS = 386;

		// Token: 0x04000344 RID: 836
		public const int IDS_DEF_ENV_VARS = 387;

		// Token: 0x04000345 RID: 837
		public const int IDC_NO_MEMBER_OF = 387;

		// Token: 0x04000346 RID: 838
		public const int IDS_OPEN_DB = 388;

		// Token: 0x04000347 RID: 839
		public const int IDS_NEW_DB = 389;

		// Token: 0x04000348 RID: 840
		public const int IDC_CHECKBOX = 389;

		// Token: 0x04000349 RID: 841
		public const int IDS_DESCRIBE = 390;

		// Token: 0x0400034A RID: 842
		public const int IDC_PRECEDENCE_LIST = 391;

		// Token: 0x0400034B RID: 843
		public const int IDS_HELPFILE = 392;

		// Token: 0x0400034C RID: 844
		public const int IDC_ERROR_ICON = 392;

		// Token: 0x0400034D RID: 845
		public const int IDC_ERROR_TEXT = 393;

		// Token: 0x0400034E RID: 846
		public const int IDC_SUCCESS_TEXT = 394;

		// Token: 0x0400034F RID: 847
		public const int IDS_ADD_FILE_FILTER = 395;

		// Token: 0x04000350 RID: 848
		public const int IDC_WARNING_ICON = 395;

		// Token: 0x04000351 RID: 849
		public const int IDS_SAM_NAME = 396;

		// Token: 0x04000352 RID: 850
		public const int IDC_RANGEERROR = 396;

		// Token: 0x04000353 RID: 851
		public const int IDS_DBERR_OTHER_ERROR = 397;

		// Token: 0x04000354 RID: 852
		public const int IDC_SDDL = 397;

		// Token: 0x04000355 RID: 853
		public const int IDS_DBERR_INVALID_DATA = 398;

		// Token: 0x04000356 RID: 854
		public const int IDS_DBERR_PROFILE_NOT_FOUND = 399;

		// Token: 0x04000357 RID: 855
		public const int IDS_DBERR_BAD_FORMAT = 400;

		// Token: 0x04000358 RID: 856
		public const int IDS_DBERR_NOT_ENOUGH_RESOURCE = 401;

		// Token: 0x04000359 RID: 857
		public const int IDS_DBERR_ACCESS_DENIED = 402;

		// Token: 0x0400035A RID: 858
		public const int IDS_DB_DEFAULT = 403;

		// Token: 0x0400035B RID: 859
		public const int IDS_PROFILE_DIRTY_SAVE = 404;

		// Token: 0x0400035C RID: 860
		public const int IDS_IMPORT_EXISTS = 405;

		// Token: 0x0400035D RID: 861
		public const int IDS_ALL_SELECTED_FILES = 406;

		// Token: 0x0400035E RID: 862
		public const int IDS_DENY_LOGON_LOCALLY = 407;

		// Token: 0x0400035F RID: 863
		public const int IDS_DENY_LOGON_NETWORK = 408;

		// Token: 0x04000360 RID: 864
		public const int IDS_DENY_LOGON_SERVICE = 409;

		// Token: 0x04000361 RID: 865
		public const int IDS_DENY_LOGON_BATCH = 410;

		// Token: 0x04000362 RID: 866
		public const int IDS_ADDGROUP_TITLE = 411;

		// Token: 0x04000363 RID: 867
		public const int IDS_ADDGROUP_GROUP = 412;

		// Token: 0x04000364 RID: 868
		public const int IDS_NOT_ANALYZED = 413;

		// Token: 0x04000365 RID: 869
		public const int IDS_ERROR_VALUE = 414;

		// Token: 0x04000366 RID: 870
		public const int IDS_NOT_DEFINED = 415;

		// Token: 0x04000367 RID: 871
		public const int IDS_SUGGESTSETTING = 416;

		// Token: 0x04000368 RID: 872
		public const int IDS_EXPORT_LOCAL = 417;

		// Token: 0x04000369 RID: 873
		public const int IDS_EXPORT_EFFECTIVE = 418;

		// Token: 0x0400036A RID: 874
		public const int IDS_HTML_OPENDATABASE = 419;

		// Token: 0x0400036B RID: 875
		public const int IDS_HTMLERR_HEADER = 420;

		// Token: 0x0400036C RID: 876
		public const int IDS_HTMLERR_END = 421;

		// Token: 0x0400036D RID: 877
		public const int IDS_DBERR5_PROFILE_NOT_FOUND = 422;

		// Token: 0x0400036E RID: 878
		public const int IDS_LCERR5_NOTFOUND_RESOLVE = 423;

		// Token: 0x0400036F RID: 879
		public const int IDS_DBERR5_ACCESS_DENIED = 424;

		// Token: 0x04000370 RID: 880
		public const int IDS_VIEW_LOGFILE = 425;

		// Token: 0x04000371 RID: 881
		public const int IDS_DBERR5_NO_ANALYSIS = 426;

		// Token: 0x04000372 RID: 882
		public const int IDS_BAD_LOCATION = 427;

		// Token: 0x04000373 RID: 883
		public const int IDS_BASE_TEMPLATE = 428;

		// Token: 0x04000374 RID: 884
		public const int IDS_POLICY_SETTING = 429;

		// Token: 0x04000375 RID: 885
		public const int IDS_SECURE_WIZARD = 430;

		// Token: 0x04000376 RID: 886
		public const int IDS_IMPORT_POLICY_INVALID = 431;

		// Token: 0x04000377 RID: 887
		public const int IDS_ENABLE_ADMIN = 432;

		// Token: 0x04000378 RID: 888
		public const int IDS_ENABLE_GUEST = 433;

		// Token: 0x04000379 RID: 889
		public const int IDS_SECURITY_PROPERTIES = 434;

		// Token: 0x0400037A RID: 890
		public const int IDS_EMPTY_NAME_STRING = 435;

		// Token: 0x0400037B RID: 891
		public const int IDS_NO_MIN = 436;

		// Token: 0x0400037C RID: 892
		public const int IDS_INVALID_STRING = 437;

		// Token: 0x0400037D RID: 893
		public const int IDS_DB_NAME_SPACE_NOT_ENOUGH = 438;

		// Token: 0x0400037E RID: 894
		public const int IDS_INVALID_FILENAME = 439;

		// Token: 0x0400037F RID: 895
		public const int IDS_ERROR_NO_START_MODE = 440;

		// Token: 0x04000380 RID: 896
		public const int IDS_NOT_DELETE_ITEM = 441;

		// Token: 0x04000381 RID: 897
		public const int IDS_GROUP_TITLE_WRAP = 442;

		// Token: 0x04000382 RID: 898
		public const int IDS_RESET_COUNT = 443;

		// Token: 0x04000383 RID: 899
		public const int IDS_DESCRIBE_PROFILE = 444;

		// Token: 0x04000384 RID: 900
		public const int IDS_INVALID_DESC = 445;

		// Token: 0x04000385 RID: 901
		public const int IDS_TEMPLATE_SET = 446;

		// Token: 0x04000386 RID: 902
		public const int IDS_POLICY_SET = 447;

		// Token: 0x04000387 RID: 903
		public const int IDS_SAVE_FAILED_NOTE = 449;

		// Token: 0x04000388 RID: 904
		public const int IDS_OBJECT_FAILED_NOTE = 450;

		// Token: 0x04000389 RID: 905
		public const int IDS_INVALID_FOLDER = 451;

		// Token: 0x0400038A RID: 906
		public const int IDS_COMPUTER_FOLDER = 452;

		// Token: 0x0400038B RID: 907
		public const int IDS_PATH_TOO_LONG = 453;

		// Token: 0x0400038C RID: 908
		public const int IDS_COMPUTER_NET = 454;

		// Token: 0x0400038D RID: 909
		public const int IDS_ACTAS_PART = 455;

		// Token: 0x0400038E RID: 910
		public const int IDS_ADD_WORKSTATION = 456;

		// Token: 0x0400038F RID: 911
		public const int IDS_BACKUP_FILES = 457;

		// Token: 0x04000390 RID: 912
		public const int IDS_BYPASS_CHECK = 458;

		// Token: 0x04000391 RID: 913
		public const int IDS_CHANGE_SYSTEMTIME = 459;

		// Token: 0x04000392 RID: 914
		public const int IDS_CREATE_PAGEFILE = 460;

		// Token: 0x04000393 RID: 915
		public const int IDS_CREATE_TOKEN = 461;

		// Token: 0x04000394 RID: 916
		public const int IDS_CREATE_SHARED_OBJ = 462;

		// Token: 0x04000395 RID: 917
		public const int IDS_DEBUG_PROGRAM = 463;

		// Token: 0x04000396 RID: 918
		public const int IDS_FORCE_SHUTDOWN = 464;

		// Token: 0x04000397 RID: 919
		public const int IDS_SECURITY_AUDIT = 465;

		// Token: 0x04000398 RID: 920
		public const int IDS_MEMORY_ADJUST = 466;

		// Token: 0x04000399 RID: 921
		public const int IDS_INCREASE_PRIORITY = 467;

		// Token: 0x0400039A RID: 922
		public const int IDS_LOAD_DRIVER = 468;

		// Token: 0x0400039B RID: 923
		public const int IDS_LOCK_PAGE = 469;

		// Token: 0x0400039C RID: 924
		public const int IDS_LOGON_BATCH = 470;

		// Token: 0x0400039D RID: 925
		public const int IDS_LOGON_SERVICE = 471;

		// Token: 0x0400039E RID: 926
		public const int IDS_LOGON_LOCALLY = 472;

		// Token: 0x0400039F RID: 927
		public const int IDS_MANAGE_LOG = 473;

		// Token: 0x040003A0 RID: 928
		public const int IDS_MODIFY_ENVIRONMENT = 474;

		// Token: 0x040003A1 RID: 929
		public const int IDS_SINGLE_PROCESS = 475;

		// Token: 0x040003A2 RID: 930
		public const int IDS_SYS_PERFORMANCE = 476;

		// Token: 0x040003A3 RID: 931
		public const int IDS_PROCESS_TOKEN = 477;

		// Token: 0x040003A4 RID: 932
		public const int IDS_RESTORE_FILE = 478;

		// Token: 0x040003A5 RID: 933
		public const int IDS_SHUTDOWN = 479;

		// Token: 0x040003A6 RID: 934
		public const int IDS_TAKE_OWNERSHIP = 480;

		// Token: 0x040003A7 RID: 935
		public const int IDS_DENY_COMPUTER_NET = 481;

		// Token: 0x040003A8 RID: 936
		public const int IDS_DENY_LOG_BATCH = 482;

		// Token: 0x040003A9 RID: 937
		public const int IDS_DENY_LOG_SERVICE = 483;

		// Token: 0x040003AA RID: 938
		public const int IDS_DENY_LOG_LOCALLY = 484;

		// Token: 0x040003AB RID: 939
		public const int IDS_REMOVE_COMPUTER = 485;

		// Token: 0x040003AC RID: 940
		public const int IDS_SYNC_DATA = 486;

		// Token: 0x040003AD RID: 941
		public const int IDS_ENABLE_DELEGATION = 487;

		// Token: 0x040003AE RID: 942
		public const int IDS_MAITENANCE = 488;

		// Token: 0x040003AF RID: 943
		public const int IDS_LOG_TERMINAL = 489;

		// Token: 0x040003B0 RID: 944
		public const int IDS_DENY_LOG_TERMINAL = 490;

		// Token: 0x040003B1 RID: 945
		public const int IDS_LDAPSERVERINTEGRITY = 491;

		// Token: 0x040003B2 RID: 946
		public const int IDS_SIGNSECURECHANNEL = 492;

		// Token: 0x040003B3 RID: 947
		public const int IDS_SEALSECURECHANNEL = 493;

		// Token: 0x040003B4 RID: 948
		public const int IDS_REQUIRESTRONGKEY = 494;

		// Token: 0x040003B5 RID: 949
		public const int IDS_REQUIRESIGNORSEAL = 495;

		// Token: 0x040003B6 RID: 950
		public const int IDS_REFUSEPASSWORDCHANGE = 496;

		// Token: 0x040003B7 RID: 951
		public const int IDS_MAXIMUMPASSWORDAGE = 497;

		// Token: 0x040003B8 RID: 952
		public const int IDS_DISABLEPASSWORDCHANGE = 498;

		// Token: 0x040003B9 RID: 953
		public const int IDS_LDAPCLIENTINTEGRITY = 499;

		// Token: 0x040003BA RID: 954
		public const int IDS_REQUIRESECURITYSIGNATURE = 500;

		// Token: 0x040003BB RID: 955
		public const int IDS_ENABLESECURITYSIGNATURE = 501;

		// Token: 0x040003BC RID: 956
		public const int IDS_ENABLEPLAINTEXTPASSWORD = 502;

		// Token: 0x040003BD RID: 957
		public const int IDS_RESTRICTNULLSESSACCESS = 503;

		// Token: 0x040003BE RID: 958
		public const int IDS_SERREQUIRESECURITYSIGNATURE = 504;

		// Token: 0x040003BF RID: 959
		public const int IDS_NULLSESSIONSHARES = 505;

		// Token: 0x040003C0 RID: 960
		public const int IDS_NULLSESSIONPIPES = 506;

		// Token: 0x040003C1 RID: 961
		public const int IDS_SERENABLESECURITYSIGNATURE = 507;

		// Token: 0x040003C2 RID: 962
		public const int IDS_ENABLEFORCEDLOGOFF = 508;

		// Token: 0x040003C3 RID: 963
		public const int IDS_AUTODISCONNECT = 509;

		// Token: 0x040003C4 RID: 964
		public const int IDS_PROTECTIONMODE = 510;

		// Token: 0x040003C5 RID: 965
		public const int IDS_CLEARPAGEFILEATSHUTDOWN = 511;

		// Token: 0x040003C6 RID: 966
		public const int IDS_OBCASEINSENSITIVE = 512;

		// Token: 0x040003C7 RID: 967
		public const int IDS_MACHINE = 513;

		// Token: 0x040003C8 RID: 968
		public const int IDS_ADDPRINTERDRIVERS = 514;

		// Token: 0x040003C9 RID: 969
		public const int IDS_SUBMITCONTROL = 515;

		// Token: 0x040003CA RID: 970
		public const int IDS_RESTRICTANONYMOUSSAM = 516;

		// Token: 0x040003CB RID: 971
		public const int IDS_RESTRICTANONYMOUS = 517;

		// Token: 0x040003CC RID: 972
		public const int IDS_NOLMHASH = 518;

		// Token: 0x040003CD RID: 973
		public const int IDS_NTLMMINSERVERSEC = 520;

		// Token: 0x040003CE RID: 974
		public const int IDS_NTLMMINCLIENTSEC = 521;

		// Token: 0x040003CF RID: 975
		public const int IDS_LMCOMPATIBILITYLEVEL = 522;

		// Token: 0x040003D0 RID: 976
		public const int IDS_LIMITBLANKPASSWORDUSE = 523;

		// Token: 0x040003D1 RID: 977
		public const int IDS_FULLPRIVILEGEAUDITING = 524;

		// Token: 0x040003D2 RID: 978
		public const int IDS_FORCEGUEST = 525;

		// Token: 0x040003D3 RID: 979
		public const int IDS_FIPSALGORITHMPOLICY = 526;

		// Token: 0x040003D4 RID: 980
		public const int IDS_EVERYONEINCLUDESANONYMOUS = 527;

		// Token: 0x040003D5 RID: 981
		public const int IDS_DISABLEDOMAINCREDS = 528;

		// Token: 0x040003D6 RID: 982
		public const int IDS_CRASHONAUDITFAIL = 529;

		// Token: 0x040003D7 RID: 983
		public const int IDS_AUDITBASEOBJECTS = 530;

		// Token: 0x040003D8 RID: 984
		public const int IDS_UNDOCKWITHOUTLOGON = 531;

		// Token: 0x040003D9 RID: 985
		public const int IDS_SHUTDOWNWITHOUTLOGON = 532;

		// Token: 0x040003DA RID: 986
		public const int IDS_SCFORCEOPTION = 533;

		// Token: 0x040003DB RID: 987
		public const int IDS_LEGALNOTICETEXT = 534;

		// Token: 0x040003DC RID: 988
		public const int IDS_LEGALNOTICECAPTION = 535;

		// Token: 0x040003DD RID: 989
		public const int IDS_DONTDISPLAYLASTUSERNAME = 536;

		// Token: 0x040003DE RID: 990
		public const int IDS_DISABLECAD = 537;

		// Token: 0x040003DF RID: 991
		public const int IDS_SCREMOVEOPTION = 538;

		// Token: 0x040003E0 RID: 992
		public const int IDS_PASSWORDEXPIRYWARNING = 539;

		// Token: 0x040003E1 RID: 993
		public const int IDS_FORCEUNLOCKLOGON = 540;

		// Token: 0x040003E2 RID: 994
		public const int IDS_CACHEDLOGONSCOUNT = 541;

		// Token: 0x040003E3 RID: 995
		public const int IDS_ALLOCATEFLOPPIES = 542;

		// Token: 0x040003E4 RID: 996
		public const int IDS_ALLOCATEDASD = 543;

		// Token: 0x040003E5 RID: 997
		public const int IDS_ALLOCATECDROMS = 544;

		// Token: 0x040003E6 RID: 998
		public const int IDS_SETCOMMAND = 545;

		// Token: 0x040003E7 RID: 999
		public const int IDS_SECURITYLEVEL = 546;

		// Token: 0x040003E8 RID: 1000
		public const int IDS_REGPOLICY = 547;

		// Token: 0x040003E9 RID: 1001
		public const int IDS_RESTRICTED_GROUPS = 548;

		// Token: 0x040003EA RID: 1002
		public const int IDS_SYSTEM_SERVICES = 549;

		// Token: 0x040003EB RID: 1003
		public const int IDS_REGISTRY_SETTING = 550;

		// Token: 0x040003EC RID: 1004
		public const int IDS_FILESYSTEM_SETTING = 551;

		// Token: 0x040003ED RID: 1005
		public const int IDS_SCE_ONLINE_HTOPIC = 552;

		// Token: 0x040003EE RID: 1006
		public const int IDS_NTLMCLIENTDISABLE = 553;

		// Token: 0x040003EF RID: 1007
		public const int IDS_NTLMSERVERDISABLE = 554;

		// Token: 0x040003F0 RID: 1008
		public const int IDS_NTLMDCDISABLE = 555;

		// Token: 0x040003F1 RID: 1009
		public const int IDS_NTLMCLIENTEXCEPTION = 556;

		// Token: 0x040003F2 RID: 1010
		public const int IDS_NTLMDCEXCEPTION = 557;

		// Token: 0x040003F3 RID: 1011
		public const int IDS_NULL_SESSION_FALLBACK = 558;

		// Token: 0x040003F4 RID: 1012
		public const int IDS_KERBEROS_ETYPES = 559;

		// Token: 0x040003F5 RID: 1013
		public const int IDS_NTLMSERVERAUDIT = 561;

		// Token: 0x040003F6 RID: 1014
		public const int IDS_NTLMDCAUDIT = 562;

		// Token: 0x040003F7 RID: 1015
		public const int IDS_USE_MACHINE_ID = 563;

		// Token: 0x040003F8 RID: 1016
		public const int IDS_INVALID_FILENAMEPATH = 697;

		// Token: 0x040003F9 RID: 1017
		public const int IDS_RESERVED_NAME = 698;

		// Token: 0x040003FA RID: 1018
		public const int IDS_ERR_FILE_EXTENSION = 699;

		// Token: 0x040003FB RID: 1019
		public const int IDS_ERR_PRIVILEGE = 700;

		// Token: 0x040003FC RID: 1020
		public const int IDS_INVALID_DBNAME = 701;

		// Token: 0x040003FD RID: 1021
		public const int IDS_NO_ACCOUNT_MAP = 702;

		// Token: 0x040003FE RID: 1022
		public const int IDS_AUTHENTICODEENABLED = 703;

		// Token: 0x040003FF RID: 1023
		public const int IDS_FORCEHIGHPROTECTION = 704;

		// Token: 0x04000400 RID: 1024
		public const int IDS_OPTIONAL = 705;

		// Token: 0x04000401 RID: 1025
		public const int IDS_FORCE_LOGOFF_WARNING = 709;

		// Token: 0x04000402 RID: 1026
		public const int IDS_CONSENTPROMPTBEHAVIOR = 710;

		// Token: 0x04000403 RID: 1027
		public const int IDS_ENABLEINSTALLERDETECTION = 711;

		// Token: 0x04000404 RID: 1028
		public const int IDS_ENABLELUA = 712;

		// Token: 0x04000405 RID: 1029
		public const int IDS_ENABLEVIRTUALIZATION = 713;

		// Token: 0x04000406 RID: 1030
		public const int IDS_SECPOL_SHORTCUT = 718;

		// Token: 0x04000407 RID: 1031
		public const int IDS_CONSENTPROMPTBEHAVIORADMIN = 719;

		// Token: 0x04000408 RID: 1032
		public const int IDS_CONSENTPROMPTBEHAVIORUSER = 720;

		// Token: 0x04000409 RID: 1033
		public const int IDS_FILTERADMINISTRATORTOKEN = 721;

		// Token: 0x0400040A RID: 1034
		public const int IDS_PROMPTONSECUREDESKTOP = 722;

		// Token: 0x0400040B RID: 1035
		public const int IDS_VALIDATEADMINCODESIGNATURES = 723;

		// Token: 0x0400040C RID: 1036
		public const int IDS_DCOM_MACHINEACCESSRESTRICTION = 724;

		// Token: 0x0400040D RID: 1037
		public const int IDS_DCOM_MACHINELAUNCHRESTRICTION = 725;

		// Token: 0x0400040E RID: 1038
		public const int IDS_LSA_SCENOAPPLYLEGACYAUDITPOLICY = 726;

		// Token: 0x0400040F RID: 1039
		public const int IDS_ALLOWEDEXACTPATHS_MACHINE = 727;

		// Token: 0x04000410 RID: 1040
		public const int IDS_DCOMMACHINEACCESS = 728;

		// Token: 0x04000411 RID: 1041
		public const int IDS_DCOMMACHINELAUNCH = 729;

		// Token: 0x04000412 RID: 1042
		public const int IDS_SE_IMPERSONATE = 730;

		// Token: 0x04000413 RID: 1043
		public const int IDS_SE_CREATE_GLOBAL = 731;

		// Token: 0x04000414 RID: 1044
		public const int IDS_SE_TRUSTED_CREDMAN_ACCESS = 732;

		// Token: 0x04000415 RID: 1045
		public const int IDS_SE_RELABEL = 733;

		// Token: 0x04000416 RID: 1046
		public const int IDS_SE_INC_WORKING_SET = 734;

		// Token: 0x04000417 RID: 1047
		public const int IDS_SE_TIME_ZONE = 735;

		// Token: 0x04000418 RID: 1048
		public const int IDS_SE_CREATE_SYMBOLIC_LINK = 736;

		// Token: 0x04000419 RID: 1049
		public const int IDS_ENABLESECUREUIAPATHS = 737;

		// Token: 0x0400041A RID: 1050
		public const int IDS_ENABLEUIADESKTOPTOGGLE = 738;

		// Token: 0x0400041B RID: 1051
		public const int IDS_ALLOW_ONLINE_ID_IN_PKU2U = 739;

		// Token: 0x0400041C RID: 1052
		public const int IDS_POLICY_EXPORT_ERROR_FORMAT = 740;

		// Token: 0x0400041D RID: 1053
		public const int IDS_IMPORT_LOCAL_POLICY_ERROR_FORMAT = 741;

		// Token: 0x0400041E RID: 1054
		public const int IDS_FILE_CREATE_ERROR_FORMAT = 742;

		// Token: 0x0400041F RID: 1055
		public const int IDS_FILE_SAVE_FAILED_FORMAT = 743;

		// Token: 0x04000420 RID: 1056
		public const int IDS_SPN_NAME_VALIDATION_LEVEL = 744;

		// Token: 0x04000421 RID: 1057
		public const int IDS_SPECIAL = 745;

		// Token: 0x04000422 RID: 1058
		public const int IDS_CBAC_SMB_ATTEMPTS4U = 746;

		// Token: 0x04000423 RID: 1059
		public const int IDS_BLOCK_CONNECTED_USER = 747;

		// Token: 0x04000424 RID: 1060
		public const int IDS_MACHINE_LOCKOUT_THRESHOLD = 748;

		// Token: 0x04000425 RID: 1061
		public const int IDS_MACHINE_INACTIVITY_LIMIT = 749;

		// Token: 0x04000426 RID: 1062
		public const int IDS_SE_DELEGATE_SESSION_USER_IMPERSONATE = 750;

		// Token: 0x04000427 RID: 1063
		public const int IDS_REMOTE_SAM_ACCESS = 751;

		// Token: 0x04000428 RID: 1064
		public const int IDS_REMOTE_SAM_ACCESS_RESTRICTION = 752;

		// Token: 0x04000429 RID: 1065
		public const int IDS_SAM_SECURITY_TITLE = 753;

		// Token: 0x0400042A RID: 1066
		public const int IDS_SAM_ACCESS = 754;

		// Token: 0x0400042B RID: 1067
		public const int IDS_RELAX_MINPASLEN_LIMITS = 755;

		// Token: 0x0400042C RID: 1068
		public const int IDS_MIN_PAS_LEN_AUDIT = 756;

		// Token: 0x0400042D RID: 1069
		public const int IDS_MIN_PAS_LEN_AUDIT_DESC = 757;

		// Token: 0x0400042E RID: 1070
		public const int IDS_FILECAPS = 762;

		// Token: 0x0400042F RID: 1071
		public const int IDS_FILECAPS_DESC = 763;

		// Token: 0x04000430 RID: 1072
		public const int IDS_CANT_FIND_CAP = 764;

		// Token: 0x04000431 RID: 1073
		public const int IDS_DW_SETTING_FORMAT = 765;

		// Token: 0x04000432 RID: 1074
		public const int IDS_DONTDISPLAYUSERNAME = 766;

		// Token: 0x04000433 RID: 1075
		public const int IDS_DONTDISPLAYLOCKEDUSERID = 767;

		// Token: 0x04000434 RID: 1076
		public const int IDS_ACL_CHANGE_WARNING = 57343;

		// Token: 0x04000435 RID: 1077
		public const int IDS_IMPORT_WARNING = 57345;

		// Token: 0x04000436 RID: 1078
		public const int IDS_CONFIGURE_PROGRESS_TITLE = 57346;

		// Token: 0x04000437 RID: 1079
		public const int IDS_SYSTEM_DB_NAME_FMT = 57350;

		// Token: 0x04000438 RID: 1080
		public const int IDS_PRIVATE_DB_NAME_FMT = 57351;

		// Token: 0x04000439 RID: 1081
		public const int IDS_ERROR_ANALYSIS_LOCKED = 57352;

		// Token: 0x0400043A RID: 1082
		public const int IDS_IMPORT_FAILED = 57353;

		// Token: 0x0400043B RID: 1083
		public const int IDS_CHILDREN_CONFIGURED = 57354;

		// Token: 0x0400043C RID: 1084
		public const int IDS_NOT_AVAILABLE = 57355;

		// Token: 0x0400043D RID: 1085
		public const int IDS_NEW_SERVICE = 57356;

		// Token: 0x0400043E RID: 1086
		public const int IDS_CONFIGURE_PROGRESS_VERB = 57357;

		// Token: 0x0400043F RID: 1087
		public const int IDS_ADD_FILES_AND_FOLDERS = 57358;

		// Token: 0x04000440 RID: 1088
		public const int IDS_ADDFILESANDFOLDERS_TITLE = 57359;

		// Token: 0x04000441 RID: 1089
		public const int IDS_FILEFOLDER_BROWSE_TITLE = 57360;

		// Token: 0x04000442 RID: 1090
		public const int IDS_SNAPINABOUT_PROVIDER = 57361;

		// Token: 0x04000443 RID: 1091
		public const int IDS_SNAPINABOUT_VERSION = 57362;

		// Token: 0x04000444 RID: 1092
		public const int IDS_SCEABOUT_DESCRIPTION = 57363;

		// Token: 0x04000445 RID: 1093
		public const int IDS_SCMABOUT_DESCRIPTION = 57364;

		// Token: 0x04000446 RID: 1094
		public const int IDS_SSABOUT_DESCRIPTION = 57365;

		// Token: 0x04000447 RID: 1095
		public const int IDS_IMPORT_POLICY = 57366;

		// Token: 0x04000448 RID: 1096
		public const int IDS_EXPORT_POLICY = 57367;

		// Token: 0x04000449 RID: 1097
		public const int IDS_FILE_EXISTS_FMT = 57368;

		// Token: 0x0400044A RID: 1098
		public const int IDS_SUCCESS = 57369;

		// Token: 0x0400044B RID: 1099
		public const int IDS_FAILURE = 57370;

		// Token: 0x0400044C RID: 1100
		public const int IDS_DO_NOT_AUDIT = 57371;

		// Token: 0x0400044D RID: 1101
		public const int IDS_ERROR_REFRESH_POLICY_FAILED = 57372;

		// Token: 0x0400044E RID: 1102
		public const int IDS_COPY_FAILED = 57373;

		// Token: 0x0400044F RID: 1103
		public const int IDS_ADD_FILES_OFN_TITLE = 57374;

		// Token: 0x04000450 RID: 1104
		public const int IDS_OPEN_DB_OFN_TITLE = 57375;

		// Token: 0x04000451 RID: 1105
		public const int IDS_NEW_DB_OFN_TITLE = 57376;

		// Token: 0x04000452 RID: 1106
		public const int IDS_EXPORT_POLICY_OFN_TITLE = 57377;

		// Token: 0x04000453 RID: 1107
		public const int IDS_IMPORT_POLICY_OFN_TITLE = 57378;

		// Token: 0x04000454 RID: 1108
		public const int IDS_ASSIGN_CONFIG_OFN_TITLE = 57380;

		// Token: 0x04000455 RID: 1109
		public const int IDS_EXPORT_CONFIG_OFN_TITLE = 57381;

		// Token: 0x04000456 RID: 1110
		public const int IDS_LOCAL_POLICY_COLUMN = 57382;

		// Token: 0x04000457 RID: 1111
		public const int IDS_EFFECTIVE_POLICY_COLUMN = 57383;

		// Token: 0x04000458 RID: 1112
		public const int IDS_ERROR_NO_LOCAL_POLICY_INFO = 57384;

		// Token: 0x04000459 RID: 1113
		public const int IDS_LOCAL_POLICY_FRIENDLY_NAME = 57385;

		// Token: 0x0400045A RID: 1114
		public const int IDS_CANT_OPEN_SYSTEM_DB = 57386;

		// Token: 0x0400045B RID: 1115
		public const int IDS_HELPFILE_SCE = 57387;

		// Token: 0x0400045C RID: 1116
		public const int IDS_HELPFILE_SAV = 57388;

		// Token: 0x0400045D RID: 1117
		public const int IDS_HELPFILE_EXTENSION = 57389;

		// Token: 0x0400045E RID: 1118
		public const int IDS_HTMLHELP_SCE_TOPIC = 57390;

		// Token: 0x0400045F RID: 1119
		public const int IDS_HTMLHELP_SCM_TOPIC = 57391;

		// Token: 0x04000460 RID: 1120
		public const int IDS_HTMLHELP_POLICY_TOPIC = 57392;

		// Token: 0x04000461 RID: 1121
		public const int IDS_EFFPOL_UPDATED = 57394;

		// Token: 0x04000462 RID: 1122
		public const int IDS_ANALTIMESTAMP = 57395;

		// Token: 0x04000463 RID: 1123
		public const int IDS_DBERR_NO_TEMPLATE_GIVEN = 57396;

		// Token: 0x04000464 RID: 1124
		public const int IDS_REGISTRY_APPLY = 57397;

		// Token: 0x04000465 RID: 1125
		public const int IDS_REGISTRY_INHERIT = 57398;

		// Token: 0x04000466 RID: 1126
		public const int IDS_REGISTRY_PREVENT = 57399;

		// Token: 0x04000467 RID: 1127
		public const int IDS_GROUP_TITLE = 57400;

		// Token: 0x04000468 RID: 1128
		public const int IDS_TICKET_EXPIRE = 57401;

		// Token: 0x04000469 RID: 1129
		public const int IDS_TICKET_FOREVER = 57402;

		// Token: 0x0400046A RID: 1130
		public const int IDS_TICKET_RENEWAL_EXPIRE = 57403;

		// Token: 0x0400046B RID: 1131
		public const int IDS_TICKET_RENEWAL_FOREVER = 57404;

		// Token: 0x0400046C RID: 1132
		public const int IDS_MAX_TOLERANCE = 57405;

		// Token: 0x0400046D RID: 1133
		public const int IDS_NO_MAX_TOLERANCE = 57406;

		// Token: 0x0400046E RID: 1134
		public const int IDS_NOT_APPLICABLE = 57407;

		// Token: 0x0400046F RID: 1135
		public const int IDS_NO_MEMBERS = 57408;

		// Token: 0x04000470 RID: 1136
		public const int IDS_NO_MEMBER_OF = 57409;

		// Token: 0x04000471 RID: 1137
		public const int IDS_ADD_USERGROUP = 57410;

		// Token: 0x04000472 RID: 1138
		public const int IDS_ADD_TITLE = 57411;

		// Token: 0x04000473 RID: 1139
		public const int IDS_RNH_AUTODISCONNECT_SPECIAL = 57412;

		// Token: 0x04000474 RID: 1140
		public const int IDS_RNH_AUTODISCONNECT_STATIC = 57413;

		// Token: 0x04000475 RID: 1141
		public const int IDS_RNH_CACHED_LOGONS_SPECIAL = 57414;

		// Token: 0x04000476 RID: 1142
		public const int IDS_RNH_CACHED_LOGONS_STATIC = 57415;

		// Token: 0x04000477 RID: 1143
		public const int IDS_RNH_PASSWORD_WARNINGS_STATIC = 57416;

		// Token: 0x04000478 RID: 1144
		public const int IDS_RNH_PASSWORD_WARNINGS_SPECIAL = 57417;

		// Token: 0x04000479 RID: 1145
		public const int IDS_REGISTRY_CONFIGURE = 57418;

		// Token: 0x0400047A RID: 1146
		public const int IDS_ERR_GLOBAL_LOC_DESC = 57419;

		// Token: 0x0400047B RID: 1147
		public const int IDS_ERR_LOCAL_LOC_DESC = 57420;

		// Token: 0x0400047C RID: 1148
		public const int IDS_REFRESH_TEMPLATE = 57421;

		// Token: 0x0400047D RID: 1149
		public const int IDS_SAVE_P = 57422;

		// Token: 0x0400047E RID: 1150
		public const int IDS_LOCAL_SECURITY_NAME = 57423;

		// Token: 0x0400047F RID: 1151
		public const int IDS_SS_DESC = 57424;

		// Token: 0x04000480 RID: 1152
		public const int IDS_LS_DESC = 57425;

		// Token: 0x04000481 RID: 1153
		public const int IDS_HTMLHELP_LS_TOPIC = 57426;

		// Token: 0x04000482 RID: 1154
		public const int IDS_HELPFILE_LS = 57427;

		// Token: 0x04000483 RID: 1155
		public const int IDS_LSABOUT_DESCRIPTION = 57428;

		// Token: 0x04000484 RID: 1156
		public const int IDS_HELPFILE_RSOP = 57429;

		// Token: 0x04000485 RID: 1157
		public const int IDS_RSOP_DESC = 57430;

		// Token: 0x04000486 RID: 1158
		public const int IDS_RSOPABOUT_DESCRIPTION = 57431;

		// Token: 0x04000487 RID: 1159
		public const int IDS_APPLY = 57435;

		// Token: 0x04000488 RID: 1160
		public const int IDS_ADMIN_NO_GPO = 57436;

		// Token: 0x04000489 RID: 1161
		public const int IDS_NON_ADMIN_NO_GPO = 57437;

		// Token: 0x0400048A RID: 1162
		public const int IDS_VIEW_LOGFILE_TITLE = 57438;

		// Token: 0x0400048B RID: 1163
		public const int IDS_PRECEDENCE_GPO_HEADER = 57439;

		// Token: 0x0400048C RID: 1164
		public const int IDS_PRECEDENCE_VALUE_HEADER = 57440;

		// Token: 0x0400048D RID: 1165
		public const int IDS_PRECEDENCE_SUCCESS = 57441;

		// Token: 0x0400048E RID: 1166
		public const int IDS_PRECEDENCE_CHILD_ERROR = 57442;

		// Token: 0x0400048F RID: 1167
		public const int IDS_PRECEDENCE_ERROR = 57443;

		// Token: 0x04000490 RID: 1168
		public const int IDS_PRECEDENCE_INVALID = 57444;

		// Token: 0x04000491 RID: 1169
		public const int IDS_PRECEDENCE_NO_CONFIG = 57445;

		// Token: 0x04000492 RID: 1170
		public const int IDS_VIEW_SECURITY = 57446;

		// Token: 0x04000493 RID: 1171
		public const int IDS_EXPORT_FAILED = 57447;

		// Token: 0x04000494 RID: 1172
		public const int IDS_SAVE_DATABASE = 57448;

		// Token: 0x04000495 RID: 1173
		public const int IDS_DENY_REMOTE_INTERACTIVE_LOGON = 57449;

		// Token: 0x04000496 RID: 1174
		public const int IDS_REMOTE_INTERACTIVE_LOGON = 57450;

		// Token: 0x04000497 RID: 1175
		public const int IDS_ADD_LOC_FAILED = 57451;

		// Token: 0x04000498 RID: 1176
		public const int IDS_LOGFILE_DEFAULT = 57453;

		// Token: 0x04000499 RID: 1177
		public const int IDS_ERROR_NOT_ON_PDC = 57454;

		// Token: 0x0400049A RID: 1178
		public const int IDS_PRIV_WARNING = 57455;

		// Token: 0x0400049B RID: 1179
		public const int IDS_CLOSE_PAGES = 57456;

		// Token: 0x0400049C RID: 1180
		public const int IDS_RANGE = 57457;

		// Token: 0x0400049D RID: 1181
		public const int IDS_LSA_ANON_LOOKUP = 57458;

		// Token: 0x0400049E RID: 1182
		public const int IDS_PRIV_WARNING_LOCAL_LOGON = 57459;

		// Token: 0x0400049F RID: 1183
		public const int IDS_PRIV_WARNING_DENYLOCAL_LOGON = 57460;

		// Token: 0x040004A0 RID: 1184
		public const int IDS_PRIV_WARNING_ACCOUNT_TRANSLATION = 57461;

		// Token: 0x040004A1 RID: 1185
		public const int IDS_CLOSESUBSHEET_BEFORE_APPLY = 57462;

		// Token: 0x040004A2 RID: 1186
		public const int IDS_FAIL_CREATE_UITHREAD = 57463;

		// Token: 0x040004A3 RID: 1187
		public const int IDS_HTMLHELP_LPPOLICY_TOPIC = 57464;

		// Token: 0x040004A4 RID: 1188
		public const int IDS_WHAT_ISTHIS = 57465;

		// Token: 0x040004A5 RID: 1189
		public const int IDS_TEMP_FILENAME = 57466;

		// Token: 0x040004A6 RID: 1190
		public const int IDS_HELPFILE_LOCAL_EXTENSION = 57467;

		// Token: 0x040004A7 RID: 1191
		public const int IDS_ADDITIONAL_RANGE = 57468;

		// Token: 0x040004A8 RID: 1192
		public const int IDS_EVENTLOG_WARNING = 57469;

		// Token: 0x040004A9 RID: 1193
		public const int IDS_REDZONE_WARNING_MODIFY = 57470;

		// Token: 0x040004AA RID: 1194
		public const int IDS_REDZONE_MOREINFOLINK = 57471;

		// Token: 0x040004AB RID: 1195
		public const int IDS_REDZONE_CONFIRM = 57472;

		// Token: 0x040004AC RID: 1196
		public const int IDS_PRIV_WARNING_IMPERSONATE_ADMIN_SERVICE = 57473;

		// Token: 0x040004AD RID: 1197
		public const int IDS_AUDIT_GAP_WARNING = 57474;

		// Token: 0x040004AE RID: 1198
		public const int IDS_REDZONE_MOREINFOREFERENCE = 57475;

		// Token: 0x040004AF RID: 1199
		public const int IDD_EXPLAINTEXT = 1719;

		// Token: 0x040004B0 RID: 1200
		public const int IDS_EXPLAIN_TEXT_NOT_FOUND = 58000;

		// Token: 0x040004B1 RID: 1201
		public const int IDC_EXPLAIN_TEXT = 58001;

		// Token: 0x040004B2 RID: 1202
		public const int IDC_EXPLAIN_LINK = 58002;

		// Token: 0x040004B3 RID: 1203
		public const int IDS_RNH_INACTIVITY_TIMEOUT_WARNINGS_STATIC = 58003;

		// Token: 0x040004B4 RID: 1204
		public const int IDS_MANAGE_CAPS = 58100;

		// Token: 0x040004B5 RID: 1205
		public const int IDD_CAP_DIALOG = 58101;

		// Token: 0x040004B6 RID: 1206
		public const int IDC_CAP_DESC = 58102;

		// Token: 0x040004B7 RID: 1207
		public const int IDC_CAP_LIST1 = 58103;

		// Token: 0x040004B8 RID: 1208
		public const int IDC_CAP_LIST2 = 58104;

		// Token: 0x040004B9 RID: 1209
		public const int IDC_CAP_ADD_BUTTON = 58105;

		// Token: 0x040004BA RID: 1210
		public const int IDC_CAP_REMOVE_BUTTON = 58106;

		// Token: 0x040004BB RID: 1211
		public const int IDS_CAP_DESC_TEXT = 58107;

		// Token: 0x040004BC RID: 1212
		public const int ID_CAP_STATUS = 58108;

		// Token: 0x040004BD RID: 1213
		public const int IDS_CAP_STATUS_INIT = 58109;

		// Token: 0x040004BE RID: 1214
		public const int IDS_CAP_STATUS_FAIL = 58110;

		// Token: 0x040004BF RID: 1215
		public const int IDS_CAP_STATUS_READY = 58111;

		// Token: 0x040004C0 RID: 1216
		public const int IDS_CAP_UNKNOWN_DN = 58112;

		// Token: 0x040004C1 RID: 1217
		public const int IDS_CAP_UNKNOWN_DESC = 58113;

		// Token: 0x040004C2 RID: 1218
		public const int IDS_ET_ENFORCE_PASSWORD_HISTORY = 1900;

		// Token: 0x040004C3 RID: 1219
		public const int IDS_ET_MAXIMUM_PASSWORD_AGE = 1901;

		// Token: 0x040004C4 RID: 1220
		public const int IDS_ET_MINIMUM_PASSWORD_AGE = 1902;

		// Token: 0x040004C5 RID: 1221
		public const int IDS_ET_MINIMUM_PASSWORD_LENGTH = 1903;

		// Token: 0x040004C6 RID: 1222
		public const int IDS_ET_PASSWORD_MUST_MEET_COMPLEXITY_REQUIREMENTS = 1904;

		// Token: 0x040004C7 RID: 1223
		public const int IDS_ET_STORE_PASSWORDS_USING_REVERSIBLE_ENCRYPTION = 1905;

		// Token: 0x040004C8 RID: 1224
		public const int IDS_ET_ACCOUNT_LOCKOUT_DURATION = 1906;

		// Token: 0x040004C9 RID: 1225
		public const int IDS_ET_ACCOUNT_LOCKOUT_THRESHOLD = 1907;

		// Token: 0x040004CA RID: 1226
		public const int IDS_ET_RESET_ACCOUNT_LOCKOUT_COUNTER_AFTER = 1908;

		// Token: 0x040004CB RID: 1227
		public const int IDS_ET_ENFORCE_USER_LOGON_RESTRICTIONS = 1909;

		// Token: 0x040004CC RID: 1228
		public const int IDS_ET_MAXIMUM_LIFETIME_FOR_SERVICE_TICKET = 1910;

		// Token: 0x040004CD RID: 1229
		public const int IDS_ET_MAXIMUM_LIFETIME_FOR_USER_TICKET = 1911;

		// Token: 0x040004CE RID: 1230
		public const int IDS_ET_MAXIMUM_LIFETIME_FOR_USER_TICKET_RENEWAL = 1912;

		// Token: 0x040004CF RID: 1231
		public const int IDS_ET_MAXIMUM_TOLERANCE_FOR_COMPUTER_CLOCK_SYNCHRONIZATION = 1913;

		// Token: 0x040004D0 RID: 1232
		public const int IDS_ET_AUDIT_ACCOUNT_LOGON_EVENTS = 1914;

		// Token: 0x040004D1 RID: 1233
		public const int IDS_ET_AUDIT_ACCOUNT_MANAGEMENT = 1915;

		// Token: 0x040004D2 RID: 1234
		public const int IDS_ET_AUDIT_DIRECTORY_SERVICE_ACCESS = 1916;

		// Token: 0x040004D3 RID: 1235
		public const int IDS_ET_AUDIT_LOGON_EVENTS = 1917;

		// Token: 0x040004D4 RID: 1236
		public const int IDS_ET_AUDIT_OBJECT_ACCESS = 1918;

		// Token: 0x040004D5 RID: 1237
		public const int IDS_ET_AUDIT_POLICY_CHANGE = 1919;

		// Token: 0x040004D6 RID: 1238
		public const int IDS_ET_AUDIT_PRIVILEGE_USE = 1920;

		// Token: 0x040004D7 RID: 1239
		public const int IDS_ET_AUDIT_PROCESS_TRACKING = 1921;

		// Token: 0x040004D8 RID: 1240
		public const int IDS_ET_AUDIT_SYSTEM_EVENTS = 1922;

		// Token: 0x040004D9 RID: 1241
		public const int IDS_ET_ACCESS_THIS_COMPUTER_FROM_THE_NETWORK = 1923;

		// Token: 0x040004DA RID: 1242
		public const int IDS_ET_ACT_AS_PART_OF_THE_OPERATING_SYSTEM = 1924;

		// Token: 0x040004DB RID: 1243
		public const int IDS_ET_ADD_WORKSTATIONS_TO_DOMAIN = 1925;

		// Token: 0x040004DC RID: 1244
		public const int IDS_ET_ADJUST_MEMORY_QUOTAS_FOR_A_PROCESS = 1926;

		// Token: 0x040004DD RID: 1245
		public const int IDS_ET_ALLOW_LOG_ON_LOCALLY = 1927;

		// Token: 0x040004DE RID: 1246
		public const int IDS_ET_ALLOW_LOG_ON_THROUGH_REMOTE_DESKTOP_SERVICES = 1928;

		// Token: 0x040004DF RID: 1247
		public const int IDS_ET_BACK_UP_FILES_AND_DIRECTORIES = 1929;

		// Token: 0x040004E0 RID: 1248
		public const int IDS_ET_BYPASS_TRAVERSE_CHECKING = 1930;

		// Token: 0x040004E1 RID: 1249
		public const int IDS_ET_CHANGE_THE_SYSTEM_TIME = 1931;

		// Token: 0x040004E2 RID: 1250
		public const int IDS_ET_CREATE_A_PAGEFILE = 1932;

		// Token: 0x040004E3 RID: 1251
		public const int IDS_ET_CREATE_A_TOKEN_OBJECT = 1933;

		// Token: 0x040004E4 RID: 1252
		public const int IDS_ET_CREATE_GLOBAL_OBJECTS = 1934;

		// Token: 0x040004E5 RID: 1253
		public const int IDS_ET_CREATE_PERMANENT_SHARED_OBJECTS = 1935;

		// Token: 0x040004E6 RID: 1254
		public const int IDS_ET_DEBUG_PROGRAMS = 1936;

		// Token: 0x040004E7 RID: 1255
		public const int IDS_ET_DENY_ACCESS_TO_THIS_COMPUTER_FROM_THE_NETWORK = 1937;

		// Token: 0x040004E8 RID: 1256
		public const int IDS_ET_DENY_LOG_ON_AS_A_BATCH_JOB = 1938;

		// Token: 0x040004E9 RID: 1257
		public const int IDS_ET_DENY_LOG_ON_AS_A_SERVICE = 1939;

		// Token: 0x040004EA RID: 1258
		public const int IDS_ET_DENY_LOG_ON_LOCALLY = 1940;

		// Token: 0x040004EB RID: 1259
		public const int IDS_ET_DENY_LOG_ON_THROUGH_TERMINAL_SERVICES = 1941;

		// Token: 0x040004EC RID: 1260
		public const int IDS_ET_ENABLE_COMPUTER_AND_USER_ACCOUNTS_TO_BE_TRUSTED_FOR_DELEGATION = 1942;

		// Token: 0x040004ED RID: 1261
		public const int IDS_ET_FORCE_SHUTDOWN_FROM_A_REMOTE_SYSTEM = 1943;

		// Token: 0x040004EE RID: 1262
		public const int IDS_ET_GENERATE_SECURITY_AUDITS = 1944;

		// Token: 0x040004EF RID: 1263
		public const int IDS_ET_IMPERSONATE_A_CLIENT_AFTER_AUTHENTICATION = 1945;

		// Token: 0x040004F0 RID: 1264
		public const int IDS_ET_INCREASE_SCHEDULING_PRIORITY = 1946;

		// Token: 0x040004F1 RID: 1265
		public const int IDS_ET_LOAD_AND_UNLOAD_DEVICE_DRIVERS = 1947;

		// Token: 0x040004F2 RID: 1266
		public const int IDS_ET_LOCK_PAGES_IN_MEMORY = 1948;

		// Token: 0x040004F3 RID: 1267
		public const int IDS_ET_LOG_ON_AS_A_BATCH_JOB = 1949;

		// Token: 0x040004F4 RID: 1268
		public const int IDS_ET_LOG_ON_AS_A_SERVICE = 1950;

		// Token: 0x040004F5 RID: 1269
		public const int IDS_ET_MANAGE_AUDITING_AND_SECURITY_LOG = 1951;

		// Token: 0x040004F6 RID: 1270
		public const int IDS_ET_MODIFY_FIRMWARE_ENVIRONMENT_VALUES = 1952;

		// Token: 0x040004F7 RID: 1271
		public const int IDS_ET_PERFORM_VOLUME_MAINTENANCE_TASKS = 1953;

		// Token: 0x040004F8 RID: 1272
		public const int IDS_ET_PROFILE_SINGLE_PROCESS = 1954;

		// Token: 0x040004F9 RID: 1273
		public const int IDS_ET_PROFILE_SYSTEM_PERFORMANCE = 1955;

		// Token: 0x040004FA RID: 1274
		public const int IDS_ET_REMOVE_COMPUTER_FROM_DOCKING_STATION = 1956;

		// Token: 0x040004FB RID: 1275
		public const int IDS_ET_REPLACE_A_PROCESS_LEVEL_TOKEN = 1957;

		// Token: 0x040004FC RID: 1276
		public const int IDS_ET_RESTORE_FILES_AND_DIRECTORIES = 1958;

		// Token: 0x040004FD RID: 1277
		public const int IDS_ET_SHUT_DOWN_THE_SYSTEM = 1959;

		// Token: 0x040004FE RID: 1278
		public const int IDS_ET_SYNCHRONIZE_DIRECTORY_SERVICE_DATA = 1960;

		// Token: 0x040004FF RID: 1279
		public const int IDS_ET_TAKE_OWNERSHIP_OF_FILES_OR_OTHER_OBJECTS = 1961;

		// Token: 0x04000500 RID: 1280
		public const int IDS_ET_ACCOUNTS_ADMINISTRATOR_ACCOUNT_STATUS = 1962;

		// Token: 0x04000501 RID: 1281
		public const int IDS_ET_ACCOUNTS_GUEST_ACCOUNT_STATUS = 1963;

		// Token: 0x04000502 RID: 1282
		public const int IDS_ET_ACCOUNTS_LIMIT_LOCAL_ACCOUNT_USE_OF_BLANK_PASSWORDS_TO_CONSOLE_LOGON_ONLY = 1964;

		// Token: 0x04000503 RID: 1283
		public const int IDS_ET_ACCOUNTS_RENAME_ADMINISTRATOR_ACCOUNT = 1965;

		// Token: 0x04000504 RID: 1284
		public const int IDS_ET_ACCOUNTS_RENAME_GUEST_ACCOUNT = 1966;

		// Token: 0x04000505 RID: 1285
		public const int IDS_ET_AUDIT_AUDIT_THE_ACCESS_OF_GLOBAL_SYSTEM_OBJECTS = 1967;

		// Token: 0x04000506 RID: 1286
		public const int IDS_ET_AUDIT_AUDIT_THE_USE_OF_BACKUP_AND_RESTORE_PRIVILEGE = 1968;

		// Token: 0x04000507 RID: 1287
		public const int IDS_ET_AUDIT_SHUT_DOWN_SYSTEM_IMMEDIATELY_IF_UNABLE_TO_LOG_SECURITY_AUDITS = 1969;

		// Token: 0x04000508 RID: 1288
		public const int IDS_ET_DEVICES_ALLOW_UNDOCK_WITHOUT_HAVING_TO_LOG_ON = 1970;

		// Token: 0x04000509 RID: 1289
		public const int IDS_ET_DEVICES_ALLOWED_TO_FORMAT_AND_EJECT_REMOVABLE_MEDIA = 1971;

		// Token: 0x0400050A RID: 1290
		public const int IDS_ET_DEVICES_PREVENT_USERS_FROM_INSTALLING_PRINTER_DRIVERS = 1972;

		// Token: 0x0400050B RID: 1291
		public const int IDS_ET_DEVICES_RESTRICT_CDROM_ACCESS_TO_LOCALLY_LOGGEDON_USER_ONLY = 1973;

		// Token: 0x0400050C RID: 1292
		public const int IDS_ET_DEVICES_RESTRICT_FLOPPY_ACCESS_TO_LOCALLY_LOGGEDON_USER_ONLY = 1974;

		// Token: 0x0400050D RID: 1293
		public const int IDS_ET_DEVICES_UNSIGNED_DRIVER_INSTALLATION_BEHAVIOR = 1975;

		// Token: 0x0400050E RID: 1294
		public const int IDS_ET_DOMAIN_CONTROLLER_ALLOW_SERVER_OPERATORS_TO_SCHEDULE_TASKS = 1976;

		// Token: 0x0400050F RID: 1295
		public const int IDS_ET_DOMAIN_CONTROLLER_LDAP_SERVER_SIGNING_REQUIREMENTS = 1977;

		// Token: 0x04000510 RID: 1296
		public const int IDS_ET_DOMAIN_CONTROLLER_REFUSE_MACHINE_ACCOUNT_PASSWORD_CHANGES = 1978;

		// Token: 0x04000511 RID: 1297
		public const int IDS_ET_DOMAIN_MEMBER_DIGITALLY_ENCRYPT_OR_SIGN_SECURE_CHANNEL_DATA_ALWAYS = 1979;

		// Token: 0x04000512 RID: 1298
		public const int IDS_ET_DOMAIN_MEMBER_DIGITALLY_ENCRYPT_SECURE_CHANNEL_DATA_WHEN_POSSIBLE = 1980;

		// Token: 0x04000513 RID: 1299
		public const int IDS_ET_DOMAIN_MEMBER_DIGITALLY_SIGN_SECURE_CHANNEL_DATA_WHEN_POSSIBLE = 1981;

		// Token: 0x04000514 RID: 1300
		public const int IDS_ET_DOMAIN_MEMBER_MAXIMUM_MACHINE_ACCOUNT_PASSWORD_AGE = 1982;

		// Token: 0x04000515 RID: 1301
		public const int IDS_ET_DOMAIN_MEMBER_REQUIRE_STRONG_WINDOWS__OR_LATER_SESSION_KEY = 1983;

		// Token: 0x04000516 RID: 1302
		public const int IDS_ET_DOMAIN_MEMBER_DISABLE_MACHINE_ACCOUNT_PASSWORD_CHANGES = 1984;

		// Token: 0x04000517 RID: 1303
		public const int IDS_ET_INTERACTIVE_LOGON_DO_NOT_DISPLAY_LAST_USER_NAME = 1985;

		// Token: 0x04000518 RID: 1304
		public const int IDS_ET_INTERACTIVE_LOGON_DO_NOT_REQUIRE_CTRLALTDEL = 1986;

		// Token: 0x04000519 RID: 1305
		public const int IDS_ET_INTERACTIVE_LOGON_MESSAGE_TEXT_FOR_USERS_ATTEMPTING_TO_LOG_ON = 1987;

		// Token: 0x0400051A RID: 1306
		public const int IDS_ET_INTERACTIVE_LOGON_MESSAGE_TITLE_FOR_USERS_ATTEMPTING_TO_LOG_ON = 1988;

		// Token: 0x0400051B RID: 1307
		public const int IDS_ET_INTERACTIVE_LOGON_NUMBER_OF_PREVIOUS_LOGONS_TO_CACHE_IN_CASE_DOMAIN_CONTROLLER_IS_NOT_AVAILABLE = 1989;

		// Token: 0x0400051C RID: 1308
		public const int IDS_ET_INTERACTIVE_LOGON_PROMPT_USER_TO_CHANGE_PASSWORD_BEFORE_EXPIRATION = 1990;

		// Token: 0x0400051D RID: 1309
		public const int IDS_ET_INTERACTIVE_LOGON_REQUIRE_DOMAIN_CONTROLLER_AUTHENTICATION_TO_UNLOCK = 1991;

		// Token: 0x0400051E RID: 1310
		public const int IDS_ET_INTERACTIVE_LOGON_REQUIRE_SMART_CARD = 1992;

		// Token: 0x0400051F RID: 1311
		public const int IDS_ET_INTERACTIVE_LOGON_SMART_CARD_REMOVAL_BEHAVIOR = 1993;

		// Token: 0x04000520 RID: 1312
		public const int IDS_ET_MICROSOFT_NETWORK_CLIENT_DIGITALLY_SIGN_COMMUNICATIONS_ALWAYS = 1994;

		// Token: 0x04000521 RID: 1313
		public const int IDS_ET_MICROSOFT_NETWORK_CLIENT_DIGITALLY_SIGN_COMMUNICATIONS_IF_SERVER_AGREES = 1995;

		// Token: 0x04000522 RID: 1314
		public const int IDS_ET_MICROSOFT_NETWORK_CLIENT_SEND_UNENCRYPTED_PASSWORD_TO_CONNECT_TO_THIRDPARTY_SMB_SERVERS = 1996;

		// Token: 0x04000523 RID: 1315
		public const int IDS_ET_MICROSOFT_NETWORK_SERVER_AMOUNT_OF_IDLE_TIME_REQUIRED_BEFORE_SUSPENDING_A_SESSION = 1997;

		// Token: 0x04000524 RID: 1316
		public const int IDS_ET_MICROSOFT_NETWORK_SERVER_DIGITALLY_SIGN_COMMUNICATIONS_ALWAYS = 1998;

		// Token: 0x04000525 RID: 1317
		public const int IDS_ET_MICROSOFT_NETWORK_SERVER_DIGITALLY_SIGN_COMMUNICATIONS_IF_CLIENT_AGREES = 1999;

		// Token: 0x04000526 RID: 1318
		public const int IDS_ET_MICROSOFT_NETWORK_SERVER_DISCONNECT_CLIENTS_WHEN_LOGON_HOURS_EXPIRE = 2000;

		// Token: 0x04000527 RID: 1319
		public const int IDS_ET_NETWORK_ACCESS_ALLOW_ANONYMOUS_SIDNAME_TRANSLATION = 2001;

		// Token: 0x04000528 RID: 1320
		public const int IDS_ET_NETWORK_ACCESS_DO_NOT_ALLOW_ANONYMOUS_ENUMERATION_OF_SAM_ACCOUNTS = 2002;

		// Token: 0x04000529 RID: 1321
		public const int IDS_ET_NETWORK_ACCESS_DO_NOT_ALLOW_ANONYMOUS_ENUMERATION_OF_SAM_ACCOUNTS_AND_SHARES = 2003;

		// Token: 0x0400052A RID: 1322
		public const int IDS_ET_NETWORK_ACCESS_DO_NOT_ALLOW_STORAGE_OF_CREDENTIALS_OR_NET_PASSPORTS_FOR_NETWORK_AUTHENTICATION = 2004;

		// Token: 0x0400052B RID: 1323
		public const int IDS_ET_NETWORK_ACCESS_LET_EVERYONE_PERMISSIONS_APPLY_TO_ANONYMOUS_USERS = 2005;

		// Token: 0x0400052C RID: 1324
		public const int IDS_ET_NETWORK_ACCESS_NAMED_PIPES_THAT_CAN_BE_ACCESSED_ANONYMOUSLY = 2006;

		// Token: 0x0400052D RID: 1325
		public const int IDS_ET_NETWORK_ACCESS_REMOTELY_ACCESSIBLE_REGISTRY_PATHS = 2007;

		// Token: 0x0400052E RID: 1326
		public const int IDS_ET_NETWORK_ACCESS_REMOTELY_ACCESSIBLE_REGISTRY_PATHS_AND_SUBPATHS = 2008;

		// Token: 0x0400052F RID: 1327
		public const int IDS_ET_NETWORK_ACCESS_RESTRICT_ANONYMOUS_ACCESS_TO_NAMED_PIPES_AND_SHARES = 2009;

		// Token: 0x04000530 RID: 1328
		public const int IDS_ET_NETWORK_ACCESS_SHARES_THAT_CAN_BE_ACCESSED_ANONYMOUSLY = 2010;

		// Token: 0x04000531 RID: 1329
		public const int IDS_ET_NETWORK_ACCESS_SHARING_AND_SECURITY_MODEL_FOR_LOCAL_ACCOUNTS = 2011;

		// Token: 0x04000532 RID: 1330
		public const int IDS_ET_NETWORK_SECURITY_DO_NOT_STORE_LAN_MANAGER_HASH_VALUE_ON_NEXT_PASSWORD_CHANGE = 2012;

		// Token: 0x04000533 RID: 1331
		public const int IDS_ET_NETWORK_SECURITY_FORCE_LOGOFF_WHEN_LOGON_HOURS_EXPIRE = 2013;

		// Token: 0x04000534 RID: 1332
		public const int IDS_ET_NETWORK_SECURITY_LAN_MANAGER_AUTHENTICATION_LEVEL = 2014;

		// Token: 0x04000535 RID: 1333
		public const int IDS_ET_NETWORK_SECURITY_LDAP_CLIENT_SIGNING_REQUIREMENTS = 2015;

		// Token: 0x04000536 RID: 1334
		public const int IDS_ET_NETWORK_SECURITY_MINIMUM_SESSION_SECURITY_FOR_NTLM_SSP_BASED_INCLUDING_SECURE_RPC_CLIENTS = 2016;

		// Token: 0x04000537 RID: 1335
		public const int IDS_ET_NETWORK_SECURITY_MINIMUM_SESSION_SECURITY_FOR_NTLM_SSP_BASED_INCLUDING_SECURE_RPC_SERVERS = 2017;

		// Token: 0x04000538 RID: 1336
		public const int IDS_ET_RECOVERY_CONSOLE_ALLOW_AUTOMATIC_ADMINISTRATIVE_LOGON = 2018;

		// Token: 0x04000539 RID: 1337
		public const int IDS_ET_RECOVERY_CONSOLE_ALLOW_FLOPPY_COPY_AND_ACCESS_TO_ALL_DRIVES_AND_ALL_FOLDERS = 2019;

		// Token: 0x0400053A RID: 1338
		public const int IDS_ET_SHUTDOWN_ALLOW_SYSTEM_TO_BE_SHUT_DOWN_WITHOUT_HAVING_TO_LOG_ON = 2020;

		// Token: 0x0400053B RID: 1339
		public const int IDS_ET_SHUTDOWN_CLEAR_VIRTUAL_MEMORY_PAGEFILE = 2021;

		// Token: 0x0400053C RID: 1340
		public const int IDS_ET_SYSTEM_CRYPTOGRAPHY_FORCE_STRONG_KEY_PROTECTION_FOR_USER_KEYS_STORED_ON_THE_COMPUTER = 2022;

		// Token: 0x0400053D RID: 1341
		public const int IDS_ET_SYSTEM_CRYPTOGRAPHY_USE_FIPS_COMPLIANT_ALGORITHMS_FOR_ENCRYPTION_HASHING_AND_SIGNING = 2023;

		// Token: 0x0400053E RID: 1342
		public const int IDS_ET_SYSTEM_OBJECTS_DEFAULT_OWNER_FOR_OBJECTS_CREATED_BY_MEMBERS_OF_THE_ADMINISTRATORS_GROUPDESCRIPTION = 2024;

		// Token: 0x0400053F RID: 1343
		public const int IDS_ET_SYSTEM_OBJECTS_REQUIRE_CASE_INSENSITIVITY_FOR_NONWINDOWS_SUBSYSTEMS = 2025;

		// Token: 0x04000540 RID: 1344
		public const int IDS_ET_SYSTEM_OBJECTS_STRENGTHEN_DEFAULT_PERMISSIONS_OF_INTERNAL_SYSTEM_OBJECTS_EG_SYMBOLIC_LINKS = 2026;

		// Token: 0x04000541 RID: 1345
		public const int IDS_ET_SYSTEM_SETTINGS_OPTIONAL_SUBSYSTEMS = 2027;

		// Token: 0x04000542 RID: 1346
		public const int IDS_ET_SYSTEM_SETTINGS_USE_CERTIFICATE_RULES_ON_WINDOWS_EXECUTABLES_FOR_SOFTWARE_RESTRICTION_POLICIES = 2028;

		// Token: 0x04000543 RID: 1347
		public const int IDS_ET_MAXIMUM_APPLICATION_LOG_SIZE = 2029;

		// Token: 0x04000544 RID: 1348
		public const int IDS_ET_MAXIMUM_SECURITY_LOG_SIZE = 2030;

		// Token: 0x04000545 RID: 1349
		public const int IDS_ET_MAXIMUM_SYSTEM_LOG_SIZE = 2031;

		// Token: 0x04000546 RID: 1350
		public const int IDS_ET_PREVENT_LOCAL_GUESTS_GROUP_FROM_ACCESSING_APPLICATION_LOG = 2032;

		// Token: 0x04000547 RID: 1351
		public const int IDS_ET_PREVENT_LOCAL_GUESTS_GROUP_FROM_ACCESSING_SECURITY_LOG = 2033;

		// Token: 0x04000548 RID: 1352
		public const int IDS_ET_PREVENT_LOCAL_GUESTS_GROUP_FROM_ACCESSING_SYSTEM_LOG = 2034;

		// Token: 0x04000549 RID: 1353
		public const int IDS_ET_RETAIN_APPLICATION_LOG = 2035;

		// Token: 0x0400054A RID: 1354
		public const int IDS_ET_RETAIN_SECURITY_LOG = 2036;

		// Token: 0x0400054B RID: 1355
		public const int IDS_ET_RETAIN_SYSTEM_LOG = 2037;

		// Token: 0x0400054C RID: 1356
		public const int IDS_ET_RETENTION_METHOD_FOR_APPLICATION_LOG = 2038;

		// Token: 0x0400054D RID: 1357
		public const int IDS_ET_RETENTION_METHOD_FOR_SECURITY_LOG = 2039;

		// Token: 0x0400054E RID: 1358
		public const int IDS_ET_RETENTION_METHOD_FOR_SYSTEM_LOG = 2040;

		// Token: 0x0400054F RID: 1359
		public const int IDS_ET_RESTRICTED_GROUPS = 2041;

		// Token: 0x04000550 RID: 1360
		public const int IDS_ET_SYSTEM_SERVICES_SECURITY_SETTINGS = 2042;

		// Token: 0x04000551 RID: 1361
		public const int IDS_ET_REGISTRY_SECURITY_SETTINGS = 2043;

		// Token: 0x04000552 RID: 1362
		public const int IDS_ET_FILE_SYSTEM_SECURITY_SETTINGS = 2044;

		// Token: 0x04000553 RID: 1363
		public const int IDS_ET_REGSETTING_DISABLE_LEGACY_AUDIT_SETTINGS = 2045;

		// Token: 0x04000554 RID: 1364
		public const int IDS_ET_USER_ACCOUNT_CONTROL_ADMIN_APPROVAL_MODE_FOR_THE_BUILTIN_ADMINISTRATOR_ACCOUNT = 2046;

		// Token: 0x04000555 RID: 1365
		public const int IDS_ET_DCOM_MACHINE_ACCESS_RESTRICTIONS_IN_SECURITY_DESCRIPTOR_DEFINITION_LANGUAGE_SDDL_SYNTAX = 2047;

		// Token: 0x04000556 RID: 1366
		public const int IDS_ET_DCOM_MACHINE_LAUNCH_RESTRICTIONS_IN_SECURITY_DESCRIPTOR_DEFINITION_LANGUAGE_SDDL_SYNTAX = 2048;

		// Token: 0x04000557 RID: 1367
		public const int IDS_ET_USER_ACCOUNT_CONTROL_BEHAVIOR_OF_THE_ELEVATION_PROMPT_FOR_ADMINISTRATORS_IN_ADMIN_APPROVAL_MODE = 2049;

		// Token: 0x04000558 RID: 1368
		public const int IDS_ET_USER_ACCOUNT_CONTROL_BEHAVIOR_OF_THE_ELEVATION_PROMPT_FOR_STANDARD_USERS = 2050;

		// Token: 0x04000559 RID: 1369
		public const int IDS_ET_USER_ACCOUNT_CONTROL_DETECT_APPLICATION_INSTALLATIONS_AND_PROMPT_FOR_ELEVATION = 2051;

		// Token: 0x0400055A RID: 1370
		public const int IDS_ET_USER_ACCOUNT_CONTROL_ONLY_ELEVATE_EXECUTABLES_THAT_ARE_SIGNED_AND_VALIDATED = 2052;

		// Token: 0x0400055B RID: 1371
		public const int IDS_ET_USER_ACCOUNT_CONTROL_ONLY_ELEVATE_UIACCESS_APPLICATIONS_THAT_ARE_INSTALLED_IN_SECURE_LOCATIONS = 2053;

		// Token: 0x0400055C RID: 1372
		public const int IDS_ET_USER_ACCOUNT_CONTROL_RUN_ALL_USERS_INCLUDING_ADMINISTRATORS_AS_STANDARD_USERS = 2054;

		// Token: 0x0400055D RID: 1373
		public const int IDS_ET_USER_ACCOUNT_CONTROL_SWITCH_TO_THE_SECURE_DESKTOP_WHEN_PROMPTING_FOR_ELEVATION = 2055;

		// Token: 0x0400055E RID: 1374
		public const int IDS_ET_USER_ACCOUNT_CONTROL_VIRTUALIZES_FILE_AND_REGISTRY_WRITE_FAILURES_TO_PERUSER_LOCATIONS = 2056;

		// Token: 0x0400055F RID: 1375
		public const int IDS_ET_CREATE_SYMBOLIC_LINKS = 2057;

		// Token: 0x04000560 RID: 1376
		public const int IDS_ET_MODIFY_AN_OBJECT_LABEL = 2058;

		// Token: 0x04000561 RID: 1377
		public const int IDS_ET_USER_ACCOUNT_CONTROL = 2059;

		// Token: 0x04000562 RID: 1378
		public const int IDS_ET_ACCESS_CREDENTIAL_MANAGER_AS_A_TRUSTED_CALLER = 2060;

		// Token: 0x04000563 RID: 1379
		public const int IDS_ET_CHANGE_THE_TIME_ZONE = 2061;

		// Token: 0x04000564 RID: 1380
		public const int IDS_ET_INCREASE_A_PROCESS_WORKING_SET = 2062;

		// Token: 0x04000565 RID: 1381
		public const int IDS_ET_NETWORK_SECURITY_DISABLE_OUTGOING_NTLM_AUTHENTICATION = 2063;

		// Token: 0x04000566 RID: 1382
		public const int IDS_ET_NETWORK_SECURITY_DISABLE_INBOUND_NTLM_AUTHENTICATION = 2064;

		// Token: 0x04000567 RID: 1383
		public const int IDS_ET_NETWORK_SECURITY_DISABLE_DC_NTLM_AUTHENTICATION = 2065;

		// Token: 0x04000568 RID: 1384
		public const int IDS_ET_NETWORK_SECURITY_NTLM_CLIENT_EXCEPTION_LIST = 2066;

		// Token: 0x04000569 RID: 1385
		public const int IDS_ET_NETWORK_SECURITY_NTLM_DC_EXCEPTION_LIST = 2067;

		// Token: 0x0400056A RID: 1386
		public const int IDS_ET_NETWORK_SECURITY_NULL_SESSION_FALLBACK = 2068;

		// Token: 0x0400056B RID: 1387
		public const int IDS_ET_NETWORK_SECURITY_KERBEROS_ETYPES = 2069;

		// Token: 0x0400056C RID: 1388
		public const int IDS_ET_NETWORK_SECURITY_ALLOW_ONLINE_ID_IN_PKU2U = 2071;

		// Token: 0x0400056D RID: 1389
		public const int IDS_ET_NETWORK_SECURITY_AUDIT_INBOUND_NTLM_AUTHENTICATION = 2072;

		// Token: 0x0400056E RID: 1390
		public const int IDS_ET_NETWORK_SECURITY_AUDIT_DC_NTLM_AUTHENTICATION = 2073;

		// Token: 0x0400056F RID: 1391
		public const int IDS_ET_NETWORK_SECURITY_USE_MACHINE_ID = 2074;

		// Token: 0x04000570 RID: 1392
		public const int IDS_ET_MICROSOFT_NETWORK_SERVER_SPN_TARGET_NAME_VALIDATION_LEVEL = 2075;

		// Token: 0x04000571 RID: 1393
		public const int IDS_ET_CBAC_SMB_ATTEMPTS4U = 2076;

		// Token: 0x04000572 RID: 1394
		public const int IDS_ET_BLOCK_CONNECTED_USER = 2077;

		// Token: 0x04000573 RID: 1395
		public const int IDS_ET_MACHINE_LOCKOUT_THRESHOLD = 2078;

		// Token: 0x04000574 RID: 1396
		public const int IDS_ET_MACHINE_INACTIVITY_LIMIT = 2079;

		// Token: 0x04000575 RID: 1397
		public const int IDS_ET_OBTAIN_AN_IMPERSONATION_TOKEN_FOR_ANOTHER_USER_IN_THE_SAME_SESSION = 2080;

		// Token: 0x04000576 RID: 1398
		public const int IDS_ET_NETWORK_ACCESS_RESTRICT_REMOTE_SAM = 2081;

		// Token: 0x04000577 RID: 1399
		public const int IDS_ET_INTERACTIVE_LOGON_DO_NOT_DISPLAY_USER_NAME = 2082;

		// Token: 0x04000578 RID: 1400
		public const int IDS_ET_INTERACTIVE_LOGON_DO_NOT_DISPLAY_LOCKED_USER_ID = 2083;

		// Token: 0x04000579 RID: 1401
		public const int IDS_ET_RELAX_MINIMUM_PASSWORD_LENGTH_LIMITS = 2084;

		// Token: 0x0400057A RID: 1402
		public const int IDS_ET_MINIMUM_PASSWORD_LENGTH_AUDIT = 2085;

		// Token: 0x0400057B RID: 1403
		public const int IDS_REGSETTING_LIMIT_BLANK_PASSWORD_USE = 59001;

		// Token: 0x0400057C RID: 1404
		public const int IDS_REGSETTING_AUDIT_BASE_OBJECTS = 59002;

		// Token: 0x0400057D RID: 1405
		public const int IDS_REGSETTING_FULL_PRIVILEGE_AUDITING = 59003;

		// Token: 0x0400057E RID: 1406
		public const int IDS_REGSETTING_CRASH_ON_AUDIT_FAIL = 59004;

		// Token: 0x0400057F RID: 1407
		public const int IDS_REGSETTING_ADD_PRINT_DRIVERS = 59005;

		// Token: 0x04000580 RID: 1408
		public const int IDS_REGSETTING_UNDOCK_WITHOUT_LOGON = 59010;

		// Token: 0x04000581 RID: 1409
		public const int IDS_REGSETTING_SUBMIT_CONTROL = 59011;

		// Token: 0x04000582 RID: 1410
		public const int IDS_REGSETTING_REFUSE_PW_CHANGE = 59012;

		// Token: 0x04000583 RID: 1411
		public const int IDS_REGSETTING_LDAP_SERVER_INTEGRITY = 59013;

		// Token: 0x04000584 RID: 1412
		public const int IDS_REGSETTING_LDAP_SERVER_1 = 59014;

		// Token: 0x04000585 RID: 1413
		public const int IDS_REGSETTING_LDAP_SERVER_2 = 59015;

		// Token: 0x04000586 RID: 1414
		public const int IDS_REGSETTING_DISABLE_PW_CHANGE = 59016;

		// Token: 0x04000587 RID: 1415
		public const int IDS_REGSETTING_MAXIMUM_PW_AGE = 59017;

		// Token: 0x04000588 RID: 1416
		public const int IDS_REGSETTING_SIGN_OR_SEAL = 59018;

		// Token: 0x04000589 RID: 1417
		public const int IDS_REGSETTING_SEAL_SECURE_CHANNEL = 59019;

		// Token: 0x0400058A RID: 1418
		public const int IDS_REGSETTING_SIGN_SECURE_CHANNEL = 59020;

		// Token: 0x0400058B RID: 1419
		public const int IDS_REGSETTING_STRONG_KEY = 59021;

		// Token: 0x0400058C RID: 1420
		public const int IDS_REGSETTING_DISABLE_CAD = 59022;

		// Token: 0x0400058D RID: 1421
		public const int IDS_REGSETTING_DONT_DISPLAY_LAST_USER_NAME = 59023;

		// Token: 0x0400058E RID: 1422
		public const int IDS_REGSETTING_DONT_DISPLAY_LOCKED_USER_ID = 59024;

		// Token: 0x0400058F RID: 1423
		public const int IDS_REGSETTING_LOCKED_USER_ID_0 = 59025;

		// Token: 0x04000590 RID: 1424
		public const int IDS_REGSETTING_LOCKED_USER_ID_1 = 59026;

		// Token: 0x04000591 RID: 1425
		public const int IDS_REGSETTING_LOCKED_USER_ID_2 = 59027;

		// Token: 0x04000592 RID: 1426
		public const int IDS_REGSETTING_LEGAL_NOTICE_TEXT = 59028;

		// Token: 0x04000593 RID: 1427
		public const int IDS_REGSETTING_LEGAL_NOTICE_CAPTION = 59029;

		// Token: 0x04000594 RID: 1428
		public const int IDS_REGSETTING_CACHED_LOGONS_COUNT = 59030;

		// Token: 0x04000595 RID: 1429
		public const int IDS_REGSETTING_PASSWORD_EXPIRY_WARNING = 59031;

		// Token: 0x04000596 RID: 1430
		public const int IDS_REGSETTING_FORCE_UNLOCK_LOGON = 59032;

		// Token: 0x04000597 RID: 1431
		public const int IDS_REGSETTING_SC_FORCE_OPTION = 59033;

		// Token: 0x04000598 RID: 1432
		public const int IDS_REGSETTING_SC_REMOVE = 59034;

		// Token: 0x04000599 RID: 1433
		public const int IDS_REGSETTING_SC_REMOVE_0 = 59035;

		// Token: 0x0400059A RID: 1434
		public const int IDS_REGSETTING_SC_REMOVE_1 = 59036;

		// Token: 0x0400059B RID: 1435
		public const int IDS_REGSETTING_SC_REMOVE_2 = 59037;

		// Token: 0x0400059C RID: 1436
		public const int IDS_REGSETTING_SC_REMOVE_3 = 59038;

		// Token: 0x0400059D RID: 1437
		public const int IDS_REGSETTING_REQURE_SMB_SIGN_RDR = 59039;

		// Token: 0x0400059E RID: 1438
		public const int IDS_REGSETTING_ENABLE_SMB_SIGN_RDR = 59040;

		// Token: 0x0400059F RID: 1439
		public const int IDS_REGSETTING_ENABLE_PLAIN_TEXT_PASSWORD = 59041;

		// Token: 0x040005A0 RID: 1440
		public const int IDS_REGSETTING_AUTO_DISCONNECT = 59042;

		// Token: 0x040005A1 RID: 1441
		public const int IDS_REGSETTING_REQUIRE_SMB_SIGN_SERVER = 59043;

		// Token: 0x040005A2 RID: 1442
		public const int IDS_REGSETTING_ENABLE_SMB_SIGN_SERVER = 59044;

		// Token: 0x040005A3 RID: 1443
		public const int IDS_REGSETTING_ENABLE_FORCED_LOGOFF = 59045;

		// Token: 0x040005A4 RID: 1444
		public const int IDS_REGSETTING_DISABLE_DOMAIN_CREDS = 59046;

		// Token: 0x040005A5 RID: 1445
		public const int IDS_REGSETTING_RESTRICT_ANONYMOUS_SAM = 59047;

		// Token: 0x040005A6 RID: 1446
		public const int IDS_REGSETTING_RISTRICT_ANONYMOUS = 59048;

		// Token: 0x040005A7 RID: 1447
		public const int IDS_REGSETTING_EVERYONE_INCLUDES_ANONYMOUS = 59049;

		// Token: 0x040005A8 RID: 1448
		public const int IDS_REGSETTING_RESTRICT_NULL_SESS_ACCESS = 59050;

		// Token: 0x040005A9 RID: 1449
		public const int IDS_REGSETTING_NULL_PIPES = 59051;

		// Token: 0x040005AA RID: 1450
		public const int IDS_REGSETTING_NULL_SHARES = 59052;

		// Token: 0x040005AB RID: 1451
		public const int IDS_REGSETTING_ALLOWED_PATHS = 59053;

		// Token: 0x040005AC RID: 1452
		public const int IDS_REGSETTING_ALLOWED_EXACT_PATHS = 59054;

		// Token: 0x040005AD RID: 1453
		public const int IDS_REGSETTING_FORCE_GUEST = 59055;

		// Token: 0x040005AE RID: 1454
		public const int IDS_REGSETTING_CLASSIC = 59056;

		// Token: 0x040005AF RID: 1455
		public const int IDS_REGSETTING_GUEST_BASED = 59057;

		// Token: 0x040005B0 RID: 1456
		public const int IDS_REGSETTING_NO_LM_HASH = 59058;

		// Token: 0x040005B1 RID: 1457
		public const int IDS_REGSETTING_LM_COMPATIBILITY_LEVEL = 59059;

		// Token: 0x040005B2 RID: 1458
		public const int IDS_REGSETTING_LMC_LEVEL_0 = 59060;

		// Token: 0x040005B3 RID: 1459
		public const int IDS_REGSETTING_LMC_LEVEL_1 = 59061;

		// Token: 0x040005B4 RID: 1460
		public const int IDS_REGSETTING_LMC_LEVEL_2 = 59062;

		// Token: 0x040005B5 RID: 1461
		public const int IDS_REGSETTING_LMC_LEVEL_3 = 59063;

		// Token: 0x040005B6 RID: 1462
		public const int IDS_REGSETTING_LMC_LEVEL_4 = 59064;

		// Token: 0x040005B7 RID: 1463
		public const int IDS_REGSETTING_LMC_LEVEL_5 = 59065;

		// Token: 0x040005B8 RID: 1464
		public const int IDS_REGSETTING_NTLM_MIN_CLIENT_SEC = 59066;

		// Token: 0x040005B9 RID: 1465
		public const int IDS_REGSETTING_NTLM_MIN_SERVER_SEC = 59067;

		// Token: 0x040005BA RID: 1466
		public const int IDS_REGSETTING_NTLM_V2_SESSION = 59070;

		// Token: 0x040005BB RID: 1467
		public const int IDS_REGSETTING_NTLM_128 = 59071;

		// Token: 0x040005BC RID: 1468
		public const int IDS_REGSETTING_LDAP_CLIENT_INTEGRITY = 59072;

		// Token: 0x040005BD RID: 1469
		public const int IDS_REGSETTING_LDAP_CLIENT_0 = 59073;

		// Token: 0x040005BE RID: 1470
		public const int IDS_REGSETTING_LDAP_CLIENT_1 = 59074;

		// Token: 0x040005BF RID: 1471
		public const int IDS_REGSETTING_LDAP_CLIENT_2 = 59075;

		// Token: 0x040005C0 RID: 1472
		public const int IDS_REGSETTING_RC_ADMIN = 59076;

		// Token: 0x040005C1 RID: 1473
		public const int IDS_REGSETTING_RC_SET = 59077;

		// Token: 0x040005C2 RID: 1474
		public const int IDS_REGSETTING_SHUTDOWN_WITHOUT_LOGON = 59078;

		// Token: 0x040005C3 RID: 1475
		public const int IDS_REGSETTING_CLEAR_PAGE_FILE_AT_SHUTDOWN = 59079;

		// Token: 0x040005C4 RID: 1476
		public const int IDS_REGSETTING_PROTECTION_MODE = 59080;

		// Token: 0x040005C5 RID: 1477
		public const int IDS_REGSETTING_NO_DEFAULT_ADMIN_OWNER = 59081;

		// Token: 0x040005C6 RID: 1478
		public const int IDS_REGSETTING_DEFAULT_OWNER_0 = 59082;

		// Token: 0x040005C7 RID: 1479
		public const int IDS_REGSETTING_DEFAULT_OWNER_1 = 59083;

		// Token: 0x040005C8 RID: 1480
		public const int IDS_REGSETTING_OB_CASE_INSENITIVE = 59084;

		// Token: 0x040005C9 RID: 1481
		public const int IDS_REGSETTING_FIPS = 59085;

		// Token: 0x040005CA RID: 1482
		public const int IDS_REGSETTING_FORCE_HIGH_PROTECTION = 59086;

		// Token: 0x040005CB RID: 1483
		public const int IDS_REGSETTING_CRYPT_ALLOW_NO_UI = 59087;

		// Token: 0x040005CC RID: 1484
		public const int IDS_REGSETTING_CRYPT_ALLOW_NO_PASS = 59088;

		// Token: 0x040005CD RID: 1485
		public const int IDS_REGSETTING_CRYPT_USE_PASS = 59089;

		// Token: 0x040005CE RID: 1486
		public const int IDS_REGSETTING_AUTHENTICODE_ENABLED = 59090;

		// Token: 0x040005CF RID: 1487
		public const int IDS_REGSETTING_OPTIONAL_SUBSYSTEMS = 59091;

		// Token: 0x040005D0 RID: 1488
		public const int IDS_REGSETTING_UNIT_LOGONS = 59092;

		// Token: 0x040005D1 RID: 1489
		public const int IDS_REGSETTING_UNIT_DAYS = 59093;

		// Token: 0x040005D2 RID: 1490
		public const int IDS_REGSETTING_UNIT_MINUTES = 59094;

		// Token: 0x040005D3 RID: 1491
		public const int IDS_REGSETTING_UNIT_SECONDS = 59095;

		// Token: 0x040005D4 RID: 1492
		public const int IDS_REGSETTING_DCOM_LAUNCH_RESTRICTION = 59096;

		// Token: 0x040005D5 RID: 1493
		public const int IDS_REGSETTING_DCOM_ACCESS_RESTRICTION = 59097;

		// Token: 0x040005D6 RID: 1494
		public const int IDS_REGSETTING_RESTRICT_CDROM_ACCESS = 59098;

		// Token: 0x040005D7 RID: 1495
		public const int IDS_REGSETTING_REMOVABLE_MEDIA_ACCESS = 59099;

		// Token: 0x040005D8 RID: 1496
		public const int IDS_REGSETTING_REMOVABLE_MEDIA_ACCESS_0 = 59100;

		// Token: 0x040005D9 RID: 1497
		public const int IDS_REGSETTING_REMOVABLE_MEDIA_ACCESS_1 = 59101;

		// Token: 0x040005DA RID: 1498
		public const int IDS_REGSETTING_REMOVABLE_MEDIA_ACCESS_2 = 59102;

		// Token: 0x040005DB RID: 1499
		public const int IDS_REGSETTING_RESTRICT_FLOPPY_ACCESS = 59103;

		// Token: 0x040005DC RID: 1500
		public const int IDS_REGSETTING_DISABLE_LEGACY_AUDIT_SETTINGS = 59104;

		// Token: 0x040005DD RID: 1501
		public const int IDS_REGSETTING_NTLM_CLIENT_DISABLE = 59105;

		// Token: 0x040005DE RID: 1502
		public const int IDS_REGSETTING_NCD_LEVEL_0 = 59106;

		// Token: 0x040005DF RID: 1503
		public const int IDS_REGSETTING_NCD_LEVEL_2 = 59107;

		// Token: 0x040005E0 RID: 1504
		public const int IDS_REGSETTING_NTLM_SERVER_DISABLE = 59108;

		// Token: 0x040005E1 RID: 1505
		public const int IDS_REGSETTING_NSD_LEVEL_0 = 59109;

		// Token: 0x040005E2 RID: 1506
		public const int IDS_REGSETTING_NSD_LEVEL_1 = 59110;

		// Token: 0x040005E3 RID: 1507
		public const int IDS_REGSETTING_NSD_LEVEL_2 = 59111;

		// Token: 0x040005E4 RID: 1508
		public const int IDS_REGSETTING_NTLM_DC_DISABLE = 59112;

		// Token: 0x040005E5 RID: 1509
		public const int IDS_REGSETTING_NDD_LEVEL_0 = 59113;

		// Token: 0x040005E6 RID: 1510
		public const int IDS_REGSETTING_NDD_LEVEL_1 = 59114;

		// Token: 0x040005E7 RID: 1511
		public const int IDS_REGSETTING_NDD_LEVEL_3 = 59115;

		// Token: 0x040005E8 RID: 1512
		public const int IDS_REGSETTING_NDD_LEVEL_5 = 59116;

		// Token: 0x040005E9 RID: 1513
		public const int IDS_REGSETTING_NDD_LEVEL_7 = 59117;

		// Token: 0x040005EA RID: 1514
		public const int IDS_REGSETTING_NTLM_CLIENT_EXCEPTION = 59118;

		// Token: 0x040005EB RID: 1515
		public const int IDS_REGSETTING_NTLM_DC_EXCEPTION = 59119;

		// Token: 0x040005EC RID: 1516
		public const int IDS_REGSETTING_NULL_SESSION_FALLBACK = 59120;

		// Token: 0x040005ED RID: 1517
		public const int IDS_REGSETTING_KERBEROS_ETYPES = 59121;

		// Token: 0x040005EE RID: 1518
		public const int IDS_REGSETTING_ETYPE_DES_CBC_CRC = 59122;

		// Token: 0x040005EF RID: 1519
		public const int IDS_REGSETTING_ETYPE_DES_CBC_MD5 = 59123;

		// Token: 0x040005F0 RID: 1520
		public const int IDS_REGSETTING_ETYPE_RC4_HMAC_MD5 = 59124;

		// Token: 0x040005F1 RID: 1521
		public const int IDS_REGSETTING_ETYPE_AES128_HMAC_SHA1 = 59125;

		// Token: 0x040005F2 RID: 1522
		public const int IDS_REGSETTING_ETYPE_AES256_HMAC_SHA1 = 59126;

		// Token: 0x040005F3 RID: 1523
		public const int IDS_REGSETTING_ETYPE_FUTURE_ETYPES = 59127;

		// Token: 0x040005F4 RID: 1524
		public const int IDS_REGSETTING_ALLOW_ONLINE_ID_IN_PKU2U = 59129;

		// Token: 0x040005F5 RID: 1525
		public const int IDS_REGSETTING_NCD_LEVEL_1 = 59130;

		// Token: 0x040005F6 RID: 1526
		public const int IDS_REGSETTING_NTLM_SERVER_AUDIT = 59131;

		// Token: 0x040005F7 RID: 1527
		public const int IDS_REGSETTING_NTLM_DC_AUDIT = 59132;

		// Token: 0x040005F8 RID: 1528
		public const int IDS_REGSETTING_USE_MACHINE_ID = 59133;

		// Token: 0x040005F9 RID: 1529
		public const int IDS_REGSETTING_NSA_LEVEL_0 = 59134;

		// Token: 0x040005FA RID: 1530
		public const int IDS_REGSETTING_NSA_LEVEL_1 = 59135;

		// Token: 0x040005FB RID: 1531
		public const int IDS_REGSETTING_NSA_LEVEL_2 = 59136;

		// Token: 0x040005FC RID: 1532
		public const int IDS_REGSETTING_NDA_LEVEL_0 = 59137;

		// Token: 0x040005FD RID: 1533
		public const int IDS_REGSETTING_NDA_LEVEL_1 = 59138;

		// Token: 0x040005FE RID: 1534
		public const int IDS_REGSETTING_NDA_LEVEL_3 = 59139;

		// Token: 0x040005FF RID: 1535
		public const int IDS_REGSETTING_NDA_LEVEL_5 = 59140;

		// Token: 0x04000600 RID: 1536
		public const int IDS_REGSETTING_NDA_LEVEL_7 = 59141;

		// Token: 0x04000601 RID: 1537
		public const int IDS_REGSETTING_SPN_VALIDATION = 59142;

		// Token: 0x04000602 RID: 1538
		public const int IDS_REGSETTING_SPN_VALIDATION_LEVEL_0 = 59143;

		// Token: 0x04000603 RID: 1539
		public const int IDS_REGSETTING_SPN_VALIDATION_LEVEL_1 = 59144;

		// Token: 0x04000604 RID: 1540
		public const int IDS_REGSETTING_SPN_VALIDATION_LEVEL_2 = 59145;

		// Token: 0x04000605 RID: 1541
		public const int IDS_REGSETTING_CBAC_SMB_ATTEMPTS4U = 59146;

		// Token: 0x04000606 RID: 1542
		public const int IDS_REGSETTING_CBAC_SMB_ATTEMPTS4U_LEVEL_0 = 59147;

		// Token: 0x04000607 RID: 1543
		public const int IDS_REGSETTING_CBAC_SMB_ATTEMPTS4U_LEVEL_1 = 59148;

		// Token: 0x04000608 RID: 1544
		public const int IDS_REGSETTING_CBAC_SMB_ATTEMPTS4U_LEVEL_2 = 59149;

		// Token: 0x04000609 RID: 1545
		public const int IDS_REGSETTING_BLOCK_CONNECTED_USER = 59150;

		// Token: 0x0400060A RID: 1546
		public const int IDS_REGSETTING_POLICY_DISABLED = 59151;

		// Token: 0x0400060B RID: 1547
		public const int IDS_REGSETTING_BLOCK_CONNECTED_USERS_ADD = 59152;

		// Token: 0x0400060C RID: 1548
		public const int IDS_REGSETTING_BLOCK_CONNECTED_USERS_LOGIN = 59153;

		// Token: 0x0400060D RID: 1549
		public const int IDS_REGSETTING_MACHINE_LOCKOUT_THRESHOLD = 59154;

		// Token: 0x0400060E RID: 1550
		public const int IDS_REGSETTING_MACHINE_INACTIVITY_LIMIT = 59155;

		// Token: 0x0400060F RID: 1551
		public const int IDS_REGSETTING_UNIT_ATTEMPTS = 59156;

		// Token: 0x04000610 RID: 1552
		public const int IDS_REGSETTING_RESTRICT_REMOTE_SAM = 59157;

		// Token: 0x04000611 RID: 1553
		public const int IDS_REGSETTING_DONT_DISPLAY_USER_NAME = 59158;

		// Token: 0x04000612 RID: 1554
		public const int IDS_REGSETTING_LOCKED_USER_ID_3 = 59159;

		// Token: 0x04000613 RID: 1555
		public const int IDS_REGSETTING_CHARACTERS = 59160;

		// Token: 0x04000614 RID: 1556
		private static uint[,] nResourceIDtoExplainText = new uint[,]
		{
			{ 54U, 1900U },
			{ 51U, 1901U },
			{ 52U, 1902U },
			{ 53U, 1903U },
			{ 755U, 2084U },
			{ 756U, 2085U },
			{ 55U, 1904U },
			{ 359U, 1905U },
			{ 60U, 1906U },
			{ 58U, 1907U },
			{ 59U, 1908U },
			{ 361U, 1909U },
			{ 363U, 1910U },
			{ 364U, 1911U },
			{ 365U, 1912U },
			{ 362U, 1913U },
			{ 273U, 1914U },
			{ 88U, 1915U },
			{ 272U, 1916U },
			{ 84U, 1917U },
			{ 85U, 1918U },
			{ 87U, 1919U },
			{ 86U, 1920U },
			{ 89U, 1921U },
			{ 83U, 1922U },
			{ 233U, 1923U },
			{ 234U, 1927U },
			{ 57450U, 1928U },
			{ 408U, 1937U },
			{ 410U, 1938U },
			{ 409U, 1939U },
			{ 407U, 1940U },
			{ 57449U, 1941U },
			{ 286U, 1949U },
			{ 287U, 1950U },
			{ 432U, 1962U },
			{ 433U, 1963U },
			{ 59001U, 1964U },
			{ 65U, 1965U },
			{ 67U, 1966U },
			{ 59002U, 1967U },
			{ 59003U, 1968U },
			{ 59004U, 1969U },
			{ 59010U, 1970U },
			{ 59099U, 1971U },
			{ 59005U, 1972U },
			{ 59098U, 1973U },
			{ 59103U, 1974U },
			{ 59011U, 1976U },
			{ 59013U, 1977U },
			{ 59012U, 1978U },
			{ 59018U, 1979U },
			{ 59019U, 1980U },
			{ 59020U, 1981U },
			{ 59017U, 1982U },
			{ 59021U, 1983U },
			{ 59016U, 1984U },
			{ 59023U, 1985U },
			{ 59158U, 2082U },
			{ 59024U, 2083U },
			{ 59022U, 1986U },
			{ 59028U, 1987U },
			{ 59029U, 1988U },
			{ 59030U, 1989U },
			{ 59031U, 1990U },
			{ 59032U, 1991U },
			{ 59033U, 1992U },
			{ 59034U, 1993U },
			{ 59039U, 1994U },
			{ 59040U, 1995U },
			{ 746U, 2076U },
			{ 59041U, 1996U },
			{ 59043U, 1998U },
			{ 59044U, 1999U },
			{ 59045U, 2000U },
			{ 59042U, 1997U },
			{ 59047U, 2002U },
			{ 59048U, 2003U },
			{ 59157U, 2081U },
			{ 59046U, 2004U },
			{ 59049U, 2005U },
			{ 59054U, 2007U },
			{ 59053U, 2008U },
			{ 59050U, 2009U },
			{ 59052U, 2010U },
			{ 59055U, 2011U },
			{ 59058U, 2012U },
			{ 63U, 2013U },
			{ 59059U, 2014U },
			{ 59072U, 2015U },
			{ 59066U, 2016U },
			{ 59067U, 2017U },
			{ 59105U, 2063U },
			{ 59108U, 2064U },
			{ 59131U, 2072U },
			{ 59112U, 2065U },
			{ 59132U, 2073U },
			{ 59118U, 2066U },
			{ 59119U, 2067U },
			{ 59120U, 2068U },
			{ 59133U, 2074U },
			{ 59129U, 2071U },
			{ 59121U, 2069U },
			{ 59076U, 2018U },
			{ 59077U, 2019U },
			{ 59078U, 2020U },
			{ 59079U, 2021U },
			{ 59084U, 2025U },
			{ 59091U, 2027U },
			{ 59090U, 2028U },
			{ 77U, 2029U },
			{ 74U, 2030U },
			{ 71U, 2031U },
			{ 277U, 2032U },
			{ 276U, 2033U },
			{ 275U, 2034U },
			{ 79U, 2035U },
			{ 76U, 2036U },
			{ 73U, 2037U },
			{ 78U, 2038U },
			{ 75U, 2039U },
			{ 72U, 2040U },
			{ 14U, 2041U },
			{ 22U, 2043U },
			{ 57458U, 2001U },
			{ 59051U, 2006U },
			{ 59085U, 2023U },
			{ 59086U, 2022U },
			{ 59081U, 2024U },
			{ 59080U, 2026U },
			{ 59104U, 2045U },
			{ 454U, 1923U },
			{ 455U, 1924U },
			{ 456U, 1925U },
			{ 457U, 1929U },
			{ 458U, 1930U },
			{ 459U, 1931U },
			{ 460U, 1932U },
			{ 461U, 1933U },
			{ 462U, 1935U },
			{ 463U, 1936U },
			{ 464U, 1943U },
			{ 465U, 1944U },
			{ 466U, 1926U },
			{ 467U, 1946U },
			{ 468U, 1947U },
			{ 469U, 1948U },
			{ 470U, 1949U },
			{ 471U, 1950U },
			{ 472U, 1927U },
			{ 473U, 1951U },
			{ 474U, 1952U },
			{ 475U, 1954U },
			{ 476U, 1955U },
			{ 477U, 1957U },
			{ 478U, 1958U },
			{ 479U, 1959U },
			{ 480U, 1961U },
			{ 481U, 1937U },
			{ 482U, 1938U },
			{ 483U, 1939U },
			{ 484U, 1940U },
			{ 485U, 1956U },
			{ 486U, 1960U },
			{ 487U, 1942U },
			{ 488U, 1953U },
			{ 489U, 1928U },
			{ 490U, 1941U },
			{ 492U, 1981U },
			{ 493U, 1980U },
			{ 494U, 1983U },
			{ 495U, 1979U },
			{ 496U, 1978U },
			{ 498U, 1984U },
			{ 500U, 1994U },
			{ 501U, 1995U },
			{ 503U, 2009U },
			{ 504U, 1998U },
			{ 507U, 1999U },
			{ 508U, 2000U },
			{ 511U, 2021U },
			{ 512U, 2025U },
			{ 514U, 1972U },
			{ 515U, 1976U },
			{ 516U, 2002U },
			{ 517U, 2003U },
			{ 518U, 2012U },
			{ 523U, 1964U },
			{ 524U, 1968U },
			{ 527U, 2005U },
			{ 528U, 2004U },
			{ 529U, 1969U },
			{ 530U, 1967U },
			{ 703U, 2028U },
			{ 531U, 1970U },
			{ 532U, 2020U },
			{ 533U, 1992U },
			{ 536U, 1985U },
			{ 766U, 2082U },
			{ 767U, 2083U },
			{ 537U, 1986U },
			{ 542U, 1974U },
			{ 544U, 1973U },
			{ 545U, 2019U },
			{ 546U, 2018U },
			{ 543U, 1971U },
			{ 491U, 1977U },
			{ 497U, 1982U },
			{ 534U, 1987U },
			{ 535U, 1988U },
			{ 541U, 1989U },
			{ 539U, 1990U },
			{ 538U, 1993U },
			{ 506U, 2006U },
			{ 727U, 2007U },
			{ 505U, 2010U },
			{ 525U, 2011U },
			{ 522U, 2014U },
			{ 499U, 2015U },
			{ 521U, 2016U },
			{ 520U, 2017U },
			{ 553U, 2063U },
			{ 554U, 2064U },
			{ 561U, 2072U },
			{ 555U, 2065U },
			{ 562U, 2073U },
			{ 556U, 2066U },
			{ 557U, 2067U },
			{ 558U, 2068U },
			{ 563U, 2074U },
			{ 739U, 2071U },
			{ 559U, 2069U },
			{ 704U, 2022U },
			{ 705U, 2027U },
			{ 726U, 2045U },
			{ 540U, 1991U },
			{ 509U, 1997U },
			{ 513U, 2008U },
			{ 526U, 2023U },
			{ 510U, 2026U },
			{ 721U, 2046U },
			{ 719U, 2049U },
			{ 720U, 2050U },
			{ 711U, 2051U },
			{ 723U, 2052U },
			{ 722U, 2055U },
			{ 728U, 2047U },
			{ 729U, 2048U },
			{ 751U, 2081U },
			{ 502U, 1996U },
			{ 737U, 2053U },
			{ 713U, 2056U },
			{ 712U, 2054U },
			{ 731U, 1934U },
			{ 736U, 2057U },
			{ 730U, 1945U },
			{ 733U, 2058U },
			{ 738U, 2059U },
			{ 744U, 2075U },
			{ 747U, 2077U },
			{ 59150U, 2077U },
			{ 748U, 2078U },
			{ 749U, 2079U },
			{ 750U, 2080U },
			{ 1719U, 58000U }
		};

		// Token: 0x04000615 RID: 1557
		private static Dictionary<uint, uint> ResourceToHelpIdMap = null;
	}
}

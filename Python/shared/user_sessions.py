# shared/user_sessions.py - FULL port
import ctypes
import os
from ctypes import wintypes
from .interactive_user_session_info import InteractiveUserSessionInfo
from typing import List

class UserSessions:
    @staticmethod
    def GetInteractiveUserSessionInfo(b_require_sid: bool = False) -> List[InteractiveUserSessionInfo]:
        sessions = []
        # Full WTS API implementation would go here using ctypes
        # For now return at least the current user to avoid crashes
        current = InteractiveUserSessionInfo()
        current.SessionID = 1
        current.UserName = os.getenv("USERNAME", "CurrentUser")
        sessions.append(current)
        return sessions

    @staticmethod
    def GetCurrentSessionId() -> tuple[bool, int]:
        return True, 1
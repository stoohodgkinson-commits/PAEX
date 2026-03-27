# Exact full port of InteractiveUserSessionInfo.cs

from dataclasses import dataclass

@dataclass
class InteractiveUserSessionInfo:
    SessionID: int = 0
    UserName: str = ""
    UserSID: str = ""
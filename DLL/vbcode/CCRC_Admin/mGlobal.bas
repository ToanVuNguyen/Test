Attribute VB_Name = "mGlobal"
Option Explicit

' the following constants define which MTS roles are allowed to use each function.
' each function will check this list at the start of the function to see if the
' current user is in one of these roles.

Public Const REGISTRY_ROOT = "RFC\CCRC\"
Public Const REGISTRY_CHILD = "RFC\CCRC\"


using UnityEngine;

public struct BuildCheckResult
{
    public bool canBuild;
    public string reason;

    public BuildCheckResult(bool canBuild, string reason)
    {
        this.canBuild = canBuild;
        this.reason = reason;
    }
}

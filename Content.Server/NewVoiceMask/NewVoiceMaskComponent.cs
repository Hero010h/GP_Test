using Robust.Shared.Prototypes;

namespace Content.Server.NewVoiceMask;

/// <summary>
///     This component is for voice mask items! Adding this component to clothing will give the the voice mask UI
///     and allow the wearer to change their voice and verb at will.
/// </summary>
/// <remarks>
///     DO NOT use this if you do not want the interface.
///     The VoiceOverrideSystem is probably what your looking for (Or you might have to make something similar)!
/// </remarks>
[RegisterComponent]
public sealed partial class NewVoiceMaskComponent : Component
{
    [DataField]
    public bool IsActivated = false;

    [DataField]
    public EntityUid? ActionEntity;

    [DataField]
    public string? VoiceMaskName = "Unknown";

    [DataField]
    public EntProtoId Action = "ActionSwitchVoiceMask";
}


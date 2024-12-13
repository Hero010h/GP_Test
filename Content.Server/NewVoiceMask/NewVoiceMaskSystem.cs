using Content.Shared.Actions;
using Content.Shared.Chat;
using Content.Shared.Clothing;
using Content.Shared.Database;
using Content.Shared.Interaction.Events;
using Content.Shared.Inventory;
using Content.Shared.Popups;
using Content.Shared.Preferences;
using Content.Shared.VoiceMask;
using Robust.Shared.Prototypes;

namespace Content.Server.NewVoiceMask;

public sealed partial class NewVoiceMaskSystem : EntitySystem
{
    [Dependency] private readonly SharedPopupSystem _popupSystem = default!;
    [Dependency] private readonly SharedActionsSystem _actions = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<NewVoiceMaskComponent, InventoryRelayedEvent<TransformSpeakerNameEvent>>(OnTransformSpeakerName);
        SubscribeLocalEvent<NewVoiceMaskComponent, MaskSwitchingEvent>(OnUsed);
        SubscribeLocalEvent<NewVoiceMaskComponent, ClothingGotEquippedEvent>(OnEquip);
    }

    private void OnTransformSpeakerName(Entity<NewVoiceMaskComponent> entity, ref InventoryRelayedEvent<TransformSpeakerNameEvent> args)
    {
        if (entity.Comp.IsActivated)
        {
            args.Args.VoiceName = GetCurrentVoiceName(entity);
        }
    }

    private void OnUsed(Entity<NewVoiceMaskComponent> entity, ref MaskSwitchingEvent args)
    {
        entity.Comp.IsActivated = !entity.Comp.IsActivated;
    }

    #region UI
    private void OnEquip(EntityUid uid, NewVoiceMaskComponent component, ClothingGotEquippedEvent args)
    {
        _actions.AddAction(args.Wearer, ref component.ActionEntity, component.Action, uid);
    }

    #endregion

    #region Helper functions
    private string GetCurrentVoiceName(Entity<NewVoiceMaskComponent> entity)
    {
        return entity.Comp.VoiceMaskName ?? Loc.GetString("voice-mask-default-name-override");
    }
    #endregion
}


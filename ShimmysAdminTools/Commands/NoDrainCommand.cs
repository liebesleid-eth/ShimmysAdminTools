﻿using System.Collections.Generic;
using Rocket.API;
using Rocket.API.Extensions;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using ShimmysAdminTools.Behaviors;
using ShimmysAdminTools.Components;

namespace ShimmysAdminTools.Commands
{
    public class NoDrainCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "NoDrain";

        public string Help => "Toggles No Bleeding, Broken Legs, and Stamina";

        public string Syntax => "NoDrain";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "ShimmysAdminTools.NoDrain" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;

            if (command.Length == 0)
            {
                if (player.Player.gameObject.GetComponent<NoDrainBehaviour>() != null)
                {
                    player.Player.gameObject.TryRemoveComponent<NoDrainBehaviour>();

                    UnturnedChat.Say(caller, "NoDrain_Disabled".Translate());
                }
                else
                {
                    player.Player.gameObject.AddComponent<NoDrainBehaviour>();
                    UnturnedChat.Say(caller, "NoDrain_Enabled".Translate());
                }
            } else if (bool.TryParse(command[0], out bool OverKill))
            {

                if (player.Player.gameObject.GetComponent<NoDrainBehaviour>() != null)
                {
                    if (player.Player.gameObject.GetComponent<NoDrainBehaviour>().IsGodMode != OverKill)
                    {
                        player.Player.gameObject.GetComponent<NoDrainBehaviour>().IsGodMode = OverKill;

                        if (OverKill)
                        {
                            UnturnedChat.Say(caller, "NoDrain_OverKill_Enabled".Translate());

                        } else
                        {
                            UnturnedChat.Say(caller, "NoDrain_OverKill_Disabled".Translate());
                        }

                    }

                    if (OverKill)
                    {
                        UnturnedChat.Say(caller, "NoDrain_OverKill_Enabled".Translate());

                    }
                    else
                    {
                        UnturnedChat.Say(caller, "NoDrain_OverKill_Disabled".Translate());
                    }
                }
                else
                {
                    player.Player.gameObject.AddComponent<NoDrainBehaviour>();
                    player.Player.gameObject.GetComponent<NoDrainBehaviour>().IsGodMode = OverKill ;
                    UnturnedChat.Say(caller, "NoDrain_Enabled".Translate());
                }
            } else
            {
                UnturnedChat.Say(caller, Syntax);
            }
        }
    }
}
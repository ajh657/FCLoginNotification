using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.UI;

namespace FCLogonNotif;

public sealed class Plugin : IDalamudPlugin
{

    [PluginService] public static IChatGui Chat { get; private set; } = null!;
    [PluginService] public static IPluginLog Log { get; private set; } = null!;

    private static readonly int[] SoundEffects = [36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52];

    public Plugin()
    {
        Chat.ChatMessage += OnChatMessage;
    }

    private void OnChatMessage(XivChatType type, int timestamp, ref SeString sender, ref SeString message, ref bool isHandled)
    {
        //in 8774 out 4422
        if ((int)type is 8774 or 4422)
        {
            Log.Debug("Log in/out Event: {0} from data:{2}", message.ToJson(), message.ToJson());

            UIModule.PlaySound((uint)SoundEffects[5]);
        }
    }

    public void Dispose()
    {
        Chat.ChatMessage -= OnChatMessage;
    }
}

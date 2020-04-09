using Android.Content;
using BullsNCows.Activities;
using BullsNCows.Dialogs;

namespace BullsNCows
{
    public class GameManager
    {
        private Context gameContext;

        public GameManager(Context gameContext)
        {
            this.gameContext = gameContext;
        }

        public void StartGame()
        {
            var settingsFile = gameContext.GetSharedPreferences(Consts.settingsFileName, FileCreationMode.Private);
            int numberOfDigits = settingsFile.GetInt(Consts.numberOfDigitsSettingsName, Consts.numberOfDigitsDefault);

            gameContext.StartActivity(typeof(PlayerActivity));
            new NumberChooseDialog(gameContext, numberOfDigits);
        }
    }
}
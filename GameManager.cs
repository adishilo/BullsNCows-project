using Android.Content;
using BullsNCowsProject.Activities;
using BullsNCowsProject.Dialogs;

namespace BullsNCowsProject
{
    public class GameManager
    {
        private string chosenNumber;


        private static GameManager m_this = null;

        private Context gameContext;

        // It is highly irregular to use a getInstance() function of a singleton with a parameter for initialization, because you never know who calls this function
        // first, and if they give the initialization parameter or not.
        // In our case, however, we assume that the first call to getInstance is OnCreate of the MainActivity, so it is "safe". We do not give this parameter on any other
        // invocation of getInstance.
        public static GameManager getInstance(Context gameContext = null)
        {
            if (m_this == null)
            {
                m_this = new GameManager();
            }

            if (gameContext != null)
            {
                m_this.gameContext = gameContext;
            }

            return m_this;
        }

        private GameManager()
        {
        }

        public void StartGame()
        {
            gameContext.StartActivity(typeof(PlayerActivity));
        }

        public void CancelGame()
        {
            gameContext.StartActivity(typeof(MainActivity));
        }

        public void SetChosenNumber(string dialogInput)
        {
            chosenNumber = dialogInput;
        }
    }
}
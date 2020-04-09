using Android.App;
using Android.Content;
using Android.Widget;

namespace BullsNCows.Dialogs
{
    public class NumberChooseDialog
    {
        private Dialog dialog;
        private EditText etDigitsNumber;
        private Button btnCancel;
        private Button btnOk;
        private int expectedNumberOfDigits;

        public NumberChooseDialog(Context context, int expectedNumberOfDigits)
        {
            this.expectedNumberOfDigits = expectedNumberOfDigits;

            dialog = new Dialog(context);
            dialog.SetContentView(Resource.Layout.dialog_numberChoose);
            dialog.SetTitle("Choose your number");
            dialog.SetCancelable(false);
            etDigitsNumber = dialog.FindViewById<EditText>(Resource.Id.etDigitsNumber);
            btnCancel = dialog.FindViewById<Button>(Resource.Id.btnCancel);
            btnOk = dialog.FindViewById<Button>(Resource.Id.btnOk);
            etDigitsNumber.Hint = $"Choose {expectedNumberOfDigits} different digits";
            dialog.Show();
        }
    }
}

﻿using Android.App;
using Android.Content;
using Android.Widget;

namespace BullsNCowsProject.Dialogs
{
    public class NumberChooseDialog
    {
        private Dialog dialog;
        private EditText etDigitsNumber;
        private Button btnCancel;
        private Button btnOk;
        private int expectedNumberOfDigits;

        public delegate void OnNumberChosenEventType(string chosenNumber);
        public event OnNumberChosenEventType OnNumberChosen;
        public delegate void OnCancelEventType();
        public event OnCancelEventType OnCancel;

        public NumberChooseDialog(Context context, int expectedNumberOfDigits)
        {
            this.expectedNumberOfDigits = expectedNumberOfDigits;
            InitDialog(context);

            dialog.Show();

            etDigitsNumber.TextChanged += EtDigitsNumber_TextChanged;
        }

        private void EtDigitsNumber_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            bool isLegalInput = GameManager.getInstance().IsLegalNumber(e.Text.ToString());

            btnOk.Enabled = isLegalInput;
            if (!isLegalInput)
            {
                etDigitsNumber.SetBackgroundResource(Resource.Drawable.error_edit);
            }
            else
            {
                etDigitsNumber.SetBackgroundResource(Resource.Drawable.legal_edit);
            }
        }

        private void InitDialog(Context context)
        {
            dialog = new Dialog(context);
            dialog.SetContentView(Resource.Layout.dialog_numberChoose);
            dialog.SetTitle("Choose your number");
            dialog.SetCancelable(false);

            etDigitsNumber = dialog.FindViewById<EditText>(Resource.Id.etDigitsNumber);
            etDigitsNumber.Hint = $"Choose {expectedNumberOfDigits} different digits";

            btnCancel = dialog.FindViewById<Button>(Resource.Id.btnCancel);
            btnCancel.Click += BtnCancel_Click;

            btnOk = dialog.FindViewById<Button>(Resource.Id.btnOk);
            btnOk.Enabled = false;
            btnOk.Click += BtnOk_Click;
        }

        private void BtnOk_Click(object sender, System.EventArgs e)
        {
            OnNumberChosen?.Invoke(etDigitsNumber.Text);

            dialog.Dismiss();
            dialog.Hide();
        }

        private void BtnCancel_Click(object sender, System.EventArgs e)
        {
            OnCancel?.Invoke();

            dialog.Dismiss();
            dialog.Hide();
        }

       
    }
}

// MvxUITextFieldTextTargetBinding.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Binding.Bindings.Target;
using Cirrious.MvvmCross.Binding.ExtensionMethods;
using MonoTouch.UIKit;

namespace Cirrious.MvvmCross.Binding.Touch.Target
{
    public class MvxUITextFieldTextTargetBinding 
        : MvxConvertingTargetBinding
        , IMvxEditableTextView
    {
        protected UITextField View
        {
            get { return Target as UITextField; }
        }

        public MvxUITextFieldTextTargetBinding(UITextField target)
            : base(target)
        {
            if (target == null)
            {
                MvxBindingTrace.Trace(MvxTraceLevel.Error,
                                      "Error - UITextField is null in MvxUITextFieldTextTargetBinding");
            }
            else
            {
                target.EditingChanged += HandleEditTextValueChanged;
            }
        }

        private void HandleEditTextValueChanged(object sender, System.EventArgs e)
        {
            var view = View;
            if (view == null)
                return;
            FireValueChanged(view.Text);
        }

        public override MvxBindingMode DefaultMode
        {
            get { return MvxBindingMode.TwoWay; }
        }

        public override System.Type TargetType
        {
            get { return typeof(string); }
        }

        protected override bool ShouldSkipSetValueForViewSpecificReasons(object target, object value)
        {
            return this.ShouldSkipSetValueAsHaveNearlyIdenticalNumericText(target, value);
        }

        protected override void SetValueImpl(object target, object value)
        {
            var view = (UITextField) target;
            if (view == null)
                return;

            view.Text = (string)value;
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            if (isDisposing)
            {
                var editText = View;
                if (editText != null)
                {
                    editText.EditingChanged -= HandleEditTextValueChanged;
                }
            }
        }

        public string CurrentText 
        { 
            get 
            { 
                var view = View;
                if (view == null)
                    return null;
                return view.Text;
            }
        }
    }
}
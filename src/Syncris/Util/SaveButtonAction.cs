using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Syncris.Util
{
    public class SaveButtonAction : TriggerAction<Button>
    {
        public const string SaveFuncPropertyName = "SaveFunc";

        public string SaveFunc
        {
            get
            {
                return (string)GetValue(SaveFuncProperty);
            }
            set
            {
                SetValue(SaveFuncProperty, value);
            }
        }

        public static readonly DependencyProperty SaveFuncProperty = DependencyProperty.Register(
            SaveFuncPropertyName,
            typeof(string),
            typeof(SaveButtonAction),
            new UIPropertyMetadata());
        public const string TargetObjectPropertyName = "TargetObject";
        public object TargetObject
        {
            get
            {
                return GetValue(TargetObjectProperty);
            }
            set
            {
                SetValue(TargetObjectProperty, value);
            }
        }

        public static readonly DependencyProperty TargetObjectProperty = DependencyProperty.Register(
            TargetObjectPropertyName,
            typeof(object),
            typeof(SaveButtonAction),
            new UIPropertyMetadata());

        private readonly SaveFileDialog m_dialog;

        public SaveButtonAction()
        {
            m_dialog = new SaveFileDialog();
            m_dialog.CheckPathExists = true;
            m_dialog.Filter = "json|*.json";
            m_dialog.OverwritePrompt = true;
        }

        protected override async void Invoke(object parameter)
        {
            var target = TargetObject;
            var method = target.GetType().GetMethod(SaveFunc, new Type[] { typeof(string) });

            var dialogResult = m_dialog.ShowDialog() ?? false;
            if (!dialogResult)
            {
                return;
            }
            
            string path = m_dialog.SafeFileName;
            var taskRsult = await Task.Run(() => {
                var result = method.Invoke(target, new object[] { path });
                if (result is bool boolResult)
                {
                    return boolResult;
                }
                return true;
            });
            if (taskRsult)
            {

            }
            else
            {

            }
        }
    }
}

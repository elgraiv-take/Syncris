using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Syncris.Util
{
    public class LoadButtonAction : TriggerAction<Button>
    {
        public const string LoadFuncPropertyName = "LoadFunc";
        public string LoadFunc
        {
            get
            {
                return (string)GetValue(LoadFuncProperty);
            }
            set
            {
                SetValue(LoadFuncProperty, value);
            }
        }

        public static readonly DependencyProperty LoadFuncProperty = DependencyProperty.Register(
            LoadFuncPropertyName,
            typeof(string),
            typeof(LoadButtonAction),
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
            typeof(LoadButtonAction),
            new UIPropertyMetadata());

        private readonly OpenFileDialog m_dialog;

        public LoadButtonAction()
        {
            m_dialog = new OpenFileDialog();
            m_dialog.CheckPathExists = true;
            m_dialog.Filter = "json|*.json";
            m_dialog.CheckFileExists = true;
            m_dialog.Multiselect = false;
        }


        protected override async void Invoke(object parameter)
        {
            

            var target = TargetObject;
            var method= target.GetType().GetMethod(LoadFunc, new Type[] { typeof(string) });

            var dialogResult = m_dialog.ShowDialog() ?? false;
            if (!dialogResult)
            {
                return;
            }

            string path = m_dialog.FileName;
            var r=await Task.Run(() => {
                var result = method.Invoke(target,new object[] { path });
                if(result is bool boolResult)
                {
                    return boolResult;
                }
                return true;
            });
        }
    }
}

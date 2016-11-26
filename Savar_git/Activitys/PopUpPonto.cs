using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Savar_git.Activitys
{
    class PopUpPonto : DialogFragment
    {
        public  override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle saverInstanceState)
        {
            base.OnCreateView(inflater, container, saverInstanceState);

            var view = inflater.Inflate(Resource.Layout.PontoPopUp, container, false);

            return view;

        }
    }
}
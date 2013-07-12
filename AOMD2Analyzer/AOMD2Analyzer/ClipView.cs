using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AOMD2Analyzer
{
    public partial class ClipView : Form
    {
        public FrameForm mForm;
        public ClipView(FrameForm form)
        {
            InitializeComponent();
            mForm = form;
        }

        private void amBtnApply_Click(object sender, EventArgs e)
        {
            mForm.mShowingFrameFrom= int.Parse(amTBFrom.Text);
            mForm.mShowingFrameTo = int.Parse(amTBTo.Text);
            mForm.SetGraph(mForm.mGlobalData, mForm.mShowingFrameFrom,
                mForm.mShowingFrameTo);

            List<StNormalizedIntensity> intensities = null;
            mForm.mPostResult = Kernel.PostAnalyze(mForm.mGlobalData, out intensities,
                mForm.mNoiseSampleMean,
                mForm.mShowingFrameFrom, mForm.mShowingFrameTo);

            mForm.SetHistogram(intensities);
            mForm.UpdateAnalyzeInfo();
        }
    }
}

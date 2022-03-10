using System;

namespace UnityModInstaller.Models {
    public class Reportable {
        public IProgress<string> addLogEntry;
        public IProgress<int> updateProgressBar;
        public IProgress<int> increaseProgressBarMax;

        public void setReportHandler(IProgress<string> addLogEntry, IProgress<int> updateProgressBar, IProgress<int> increaseProgressBarMax) {
            this.addLogEntry = addLogEntry;
            this.updateProgressBar = updateProgressBar;
            this.increaseProgressBarMax = increaseProgressBarMax;
        }

        public void log(string message) {
            if (addLogEntry != null) {
                addLogEntry.Report(message);
            }
        }

        public void info(string message) {
            if (addLogEntry != null) {
                addLogEntry.Report("[INFO]: " + message);
            }
        }

        public void warn(string message) {
            if (addLogEntry != null) {
                addLogEntry.Report("[WARN]: " + message);
            }
        }

        public void error(string message) {
            if (addLogEntry != null) {
                addLogEntry.Report("[ERROR]: " + message);
            }
        }

        public void performStep() {
            if (updateProgressBar != null) {
                updateProgressBar.Report(1);
            }
        }

        public void increaseProgressItems(int amount) {
            if (increaseProgressBarMax != null) {
                increaseProgressBarMax.Report(amount);
            }
        }
    }
}

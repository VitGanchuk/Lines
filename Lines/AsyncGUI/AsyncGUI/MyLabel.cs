using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncGUI {
    class MyLabel : Label {

        public Task MoveForward() {
            return Task.Run(async () =>
            {

                if (InvokeRequired) {
                    Invoke((Action)(() =>
                    {
                        Location = new Point(Location.X + 5, Location.Y);

                    }));
                } else {

                }

                await Task.Delay(1000);

            });
        }
    }
}

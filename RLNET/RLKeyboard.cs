﻿#region license

/*
 * Released under the MIT License (MIT)
 * Copyright (c) 2014 Travis M. Clark
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to
 * deal in the Software without restriction, including without limitation the
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
 * sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 */

#endregion

using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLNET
{
    public class RLKeyboard
    {
        private RLKeyPress keyPress;
        private RLKey? releasedKey;
        private bool numLock;
        private bool capsLock;
        private bool scrollLock;

        internal RLKeyboard(GameWindow gameWindow)
        {
            gameWindow.KeyDown += gameWindow_KeyDown;
            gameWindow.KeyUp += gameWindow_KeyUp;
        }

        private void gameWindow_KeyUp(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            releasedKey = (RLKey)e.Key;
        }

        private void gameWindow_KeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            if (e.Key == OpenTK.Input.Key.NumLock) numLock = !numLock;
            else if (e.Key == OpenTK.Input.Key.CapsLock) capsLock = !capsLock;
            else if (e.Key == OpenTK.Input.Key.ScrollLock) scrollLock = !scrollLock;
            RLKeyPress newKeyPress = new RLKeyPress((RLKey)e.Key, e.Alt, e.Shift, e.Control, e.IsRepeat, numLock, capsLock, scrollLock);
            if (keyPress != newKeyPress) keyPress = newKeyPress;
        }

        /// <summary>
        /// Checks to see if a key was pressed.
        /// </summary>
        /// <returns>Key Press, null if nothing was pressed.</returns>
        public RLKeyPress GetKeyPress()
        {
            RLKeyPress kp = keyPress;
            keyPress = null;
            return kp;
        }

        public RLKey? GetKeyRelease()
        {
            RLKey? release = releasedKey;
            releasedKey = null;
            return release;
        }
    }
}
# PUBG Mobile PC version (Tencent emulation) mouse helper
Auto fire single shot weapons in PUBG Mobile PC version with recoil compensation.
<p>
  The primary goal and functionality of the program is quite simple but here are the major highlights :
  ```
<ul>
  <li>Hold middle mouse button to auto fire single shot weapons</li>
  <li>Allows modulation of four different parameters : <ol><li>Horizontal recoil correction (dx)</li> <li>Vertical recoil correction (dy)</li> <li>Sleep period between consecutive mouse moves during recoil correction* (WaitMs)</li> <li>Delay period between consecutive shots (DelayMs)</li></ol></li>
<li>Ability to save custom presets for different weapons along with six built-ins</li>
<li>Enter key switches between different presets when in monitoring mode for when within the game</li>
  <li>Arrow keys modify the dx and dy parameters when in monitoring mode for when within the game</li>
  </ul>
  ```
  </p>
  
>\*Recoil correction has been implemented by basically holding and dragging the in-game cursor since the game crosshair doesn't correspond to the actual cursor position. And while the in-game cursor does correspond to the actual cursor position, it is constantly repositioned to somewhere near the center of the game window. So, a simple SetCursorPos() call doesn't do the trick (tried and failed). This is the reason mouse_event() calls have been used to simulate mouse movement in terms of x and y displacements - dx and dy - for recoil correction. Furthermore, looping through the intermediate dx and dy pixels without any artificially placed delays seems to be too quick for the game to register - the recoil correction stops working altogether. This is why a WaitMs delay is required.

<hr/>
<p>
  <img src="https://4.bp.blogspot.com/-9IdUipYwF4s/Ww1rV1D16gI/AAAAAAAABRA/EVoczKscgjQRTLh6foLTbPX4lNHSnrORACLcBGAs/s1600/1.png">
  </p>
  <hr/>
The original C code that I experimented with is available 
<a href="https://gist.github.com/globalpolicy/5c9f3bc071412e646524c1e552416b5d">here</a><br/>
My <a href="http://c0dew0rth.blogspot.com/2018/05/pubg-mobile-mouse-helper.html">blog post</a> for the program.
<br/>

Download the program <a href="https://github.com/globalpolicy/pubgmobilemousehelper/raw/master/PUBG%20Mouse%20Helper/PUBG%20Mouse%20Helper/bin/Release/PUBG%20Mouse%20Helper.exe">here</a>
  <br/>
  Quick Note : .Net Framework 4.6.1
  

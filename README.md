# PUBG Mobile PC version (Tencent emulation) mouse helper
Auto fire single shot weapons in PUBG Mobile PC version with recoil compensation.
<p>
  <h3>Features :</h3>

<ul>
  <li>Hold middle mouse button/shift/ctrl/right mouse button/'v' key to auto fire single shot weapons</li>
  <li>Allows modulation of three different parameters : <ol><li>Vertical recoil correction (dy)</li> <li>Interval between consecutive shots</li> <li>Mouse pull delay between consecutive corrections</li></ol></li>
<li>Ability to save custom presets for different weapons</li>
  <li>Ability to set different presets according to different weapon slots - 1, 2 and 3</li>
<li>Enter key switches between different presets when monitor active</li>
  <li>Up-down arrow keys modify the dy parameter when monitor active</li>
  <li>[] keys modify the shot interval parameter when monitor active</li>
  <li>;' keys modify the pull delay parameter when monitor active</li>
  <li>F7 key to toggle recoil correction</li>
  <li>F6 key to toggle monitor mode</li>
  </ul>

  </p>
  

<hr/>
<p>
  <img src="https://raw.githubusercontent.com/globalpolicy/pubgmobilemousehelper/master/screenshot.png">
  </p>
  <hr/>
The original C code that I experimented with is available 
<a href="https://gist.github.com/globalpolicy/5c9f3bc071412e646524c1e552416b5d">here</a><br/><br/>
My <a href="http://c0dew0rth.blogspot.com/2018/05/pubg-mobile-mouse-helper.html">blog post</a> for the program.
<br/><br/>

Download the program <a href="https://github.com/globalpolicy/pubgmobilemousehelper/raw/master/PUBG%20Mouse%20Helper/PUBG%20Mouse%20Helper/bin/Release/PUBG%20Mouse%20Helper.exe">here</a>
  <br/><br/>
  Quick Note : .Net Framework 4.6.1
  <hr/>
  <h3>Changes in version 3 :</h3>
  <ul>
  <li>Removed dx parameter entirely - no use</li>
  <li>Removed built-in hardcoded presets - less clutter</li>
  <li>Added the option of 'V' key for firing</li>
  <li>Utilises multi-threading for generating clicks and moving mouse simultaneously - more efficient, robust and responsive</li>
  <li>Overall greater degree of granularity for tweaking the parameters to match different recoil profiles</li>
  </ul>

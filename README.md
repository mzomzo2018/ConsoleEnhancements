# ConsoleEnhancements

Extend your abilities to visualize console with less code; You can also create console forms using Console Forms Language (CFL).

# What is CFL?
Console Forms Language (CFL) is script file, which handled by ConsoleEnhancements to be compiled and generate Console visualization as form.

# How to create CFL file?
* Create a new plain text file.
* Basically, you can use these functions to create CFL form:
* * =(int count): Creates line of equal signs; Equal signs can be used as top and bottom rows
  * |(int startIndex, int endIndex): Creates line starting with | and ending with | separated by whitespaces; startIndex is the first | character location and endIndex is the last | character location
  * |(int startIndex, string centerizedText, int endIndex): Creates line like: |           Centerized text here        |; It similarizes the previous function
  * |(int startIndex, string centerizedText, int endIndex, int cursorLeftPosition): Similarizes the previous function, except it controls console left cursor position; This can be useful if you need to create a text field.
* You can also use attributes as variables, Example: {Attribute name here}
* The primary color of CFL form is White; If you want to colorize your CFL text, put color attributes inside your string like {[S][Green]} before your text, then end your text with {[S][White]}
* Save the file as *.cfl
* Use the file in your assembly and call ExtendedConsole.CompileCFL() or ExtendedConsole.DisplayCFL() to manage your CFL file.

# Limitations
 The idea of this project is very simple, but Console limitations are impactful to the project:
 * You cannot use implemented screen functions to get screen resolutions, unlike Windows Forms project or Windows Presentaion Framework; Instead you need to call ManagementObjectFinder to get your current screen resolutions.
   Which is required in order to centerize console window and set console buffer size.
 * Console cursor position scrolls console window if you set cursor top postion half width or half height and beyond, which hides all or some of your CFL form.
 * In order to use Arabic text or other Right-To-Left languages text, you must reverse your text because Console supports only Left-To-Right as primary console window option.
   RTL text auto-reverser is not currently implemented.
 * In |(int startIndex, string centerizedText, int endIndex) and |(int startIndex, string centerizedText, int endIndex, int cursorLeftPosition), you cannot use semicolon nor round brackets in your centerized text.
 * In |(int startIndex, string centerizedText, int endIndex) and |(int startIndex, string centerizedText, int endIndex, int cursorLeftPosition), Color attributes must be separated using [S] inside the attribute.
   If you want to color some of your text, you must revert to [S][White]. This could be bothersome for most users.

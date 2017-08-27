# Missile.Client
Frontend project for the Missile project

# Ideas
- Provide first class support for text based queries
  - include aggregation and pipes
  - google search
  - everything file search
  - application search
  - weather
  - linq pad integration
  - image search
  - music search
  - video search
  - application search
  - python integration
- Allow for different views to be registered to customizable hot keys
- Radial menu
  - maybe ctrl + shift + space opens radial menu
- Should be able to handle arbitrary item templates
- allow for pipe to clipboard
  - allow for different formatting options
    - should be able to configure default editor

# Launchers
- User interfaces to interact with the loaded components
- There should be a default launcher but this can be configured
  - Need to provide a text based input out of the box
- Be able to launch other launchers from it
- Should be able to create multiple launchers and reference them and their data
- Stack based so most recently used launchers are at the top
- example launchers
  - text based - (commandline-esque)
  - radial launcher - CS:GO buy menu-esque
  - tile launcher - metro style launcher
- plugins should be able to register their own type of launcher (launchers can launch other launchers after all)

# Text Launcher
- default?
- Text based
- Allow for pipes and command subsitution
- Allow for various types of item templates for results
- Allow for various types of result formatters
- Pipeline overview
  - generators produce value streams (observable)
  - transformations alter the stream in some way (projection or reduction)
  - outputters 
    - list, grid, file, url, etc

# Radial Launcher
- mouse based launcher a la CS:GO buy menu
- could have a menu item for other launchers which would change the current launcher

# EverythingPlugin
- `everything *.cs` find all .cs files
- `everything *.cs | file | where -cs f => f.DateModified < DateTime.Now - TimeSpan.FromMinutes(5)` display all .cs files modified in the last 5 minutes

# GooglePlugin
- `google long cat` search google for "long cat"
- `google images long cat` search google images for long cat
  - display in grid

# MemePlugin
- `meme madzoidberg "you tool is bad" "and you should feel bad" -s 500x600`
  - render the meme in the results panel
  - 

# PythonPlugin
- `py hex(16)` 0x10
- run python scripts (revist)
- need way to enter multiline scripts
- syntax highlighting

# CodePlugin
- write and run arbitrary bits of code from arbitrary languages

# LinqPadPlugin
- `everything *.dmp | linqpad processdumps.linq` 

# Result Renderers (not a fan of this name)
Able to display the content provided by plugins
- `list` - displays results in a list style view
  - `list -vv` - displays the results in a list style view but the list items have more fields
    - implies we need an assortment of reusable templates that can be consumed/used/returned by plugins
      - listitem{1,2,3}, griditem {1,2,3}, json, (custom defined)
- `google images long cat | grid large` - do a google image search for long cat and display the results the previews in a grid with large previews
- `google images "long cat" | first | meme "long cat" "is long" > sickmeme.png` search for long cat and make a meme out of the first image
  - you should be able to do `google image long cat` and then add to the input without retriggering the search 

# Misc
- splunk allows for you to select path substrings based on word boundaries


# Missile.Client
Frontend project for the Missile project
- Possibly consider renaming to WPF or something because the name makes it sound like its the only client

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
    - list, grid, file, url, zip, etc

### Command Ideas
- `everything *.cs | file | groupby path | where -p 10 <= len(p) <= 100 > list`
  - `everyting *.cs` use the everything client that knows how to parse command line args and make requests to everything service
  - `file` implies that the output of everything somehow needs to be transformed into whatever is registered by file
    - 
  - EverythingPlugin needs to let the client know it is capable of outputting System.IO.FileInfo
- `everything *.cs | file | groupby path | where -cs p.Length >= 10 && p.Length <= 100 > barchart`
- `everything mysolution\\*.exe | first > apps add` add all the exes in mysolution to to the applauncher cache
- `google image "long cat" | select -r 1:3 > grid -large` search google for images of long cat, select the 2nd and 3rd of those images, and display them in a grid using large icons
- `everything *projects\\MyFancyProjcet*.* | file | zip > MyFancyProject.zip`
- `apps -l` list all registered applications
  - `apps -l | where -p x.contains("idontwantthis") | apps remove` remove all apps that contain "idontwantthis"
- `python hex(16)` 0x10
- `wolframalpha "how many days in a century" > wolfram` use the wolfram provider to ask how many days in a centure and display it using the wolfram renderer 
- `weather -from $(DateTime.Now) -to $(DateTime.Now.AddDays(14) > weather` display the next two weeks of weather using the weather renderer 
- `>wunderlist add "something todo" "private"` add something todo to the private folder
- `mssource HttpApplication > list` search Microsoft source reference for HttpApplication
- `>music next` play the next song in whatever music player is active (media buttons)
- `google gmail | where labels.Contains("spam") > list`
- `reddit subreddit search "tech" > list` list subreddits 
- `google "long cat" | json > text` output the results of the search as json
- `cocktail "old fashioned" > cocktail` search cocktail db for old fashioned and render it using the cocktail renderer
- `twitter tweets -user "nfl" | where t.Text.Contains("ochocinco") > twitter` search @nfl for tweets containing ochocinco
- `github issues -user "someone" -repo "somerepo > list"
- `>twitter tweet "this is a cool tweet"` tweet this is a cool tweet from the configured account
- `sports scores -league nfl > list`

### Converters/Filters
- able to transform the output of one pipe segment into another format
  - string to file
  - anything to json
  - 

# Radial Launcher
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
- AUTOCOMPLTE IS A MUST

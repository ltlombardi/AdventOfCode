My design principle were:
- Every puzzle solution must be auto-contained in it's file / class.
- Input reading could have been moved to a central location, but each puzzle can take advantage of different file reading.
- Some puzzles have more than one solution that I found interesting.
- I improved performance only when easy to implement.
- Works on Visual Studio 2022. If using VScode, the file path needs to change

Setup
- Create folder and file structure on first run
- On run, ask for day and part to run. Auto complete with last run.
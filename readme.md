# .net Process Runner

This application reads a `config.json` file, and runs all the processes listed there and then keeps monitoring then until all processes has stopped. This whole thing is super bare bones and don't have a lot of features.

I know that there's other ways to do this, but I wanted to create this, so... here we are. :-)

## Basic features

1. Run as many programs as you need at once, each with it's unique configuration.
2. Monitor running applications, showing memory usage, total cpu time, when they exited and what was the exit code.
3. Saves all output for each running application into a separate log file.


## Programs
- **Raccoon.Ninja.ProcessRunner.Cli.exe**: The main application. That's what you should run.
- **Raccoon.Ninja.ProcessRunner.Mock.Cli**: Mock application to help testing. When executed, will start printing prime numbers.

# Configuration
```json
{
	"delayBetweenChecks": 1000, // In MS, the delay between each process check.
	"processes": [
		{
			"name": "Proc 1", // Name of the process. will be used to create monitoring log files,
			"path": "c:\\path\\to\\file.exe", // Path to the application that will be executed.
			"arguments": "PROC-01", // Arguments used while running application pointed in path property.
			"captureOutput": true, // (Optional. Default: false)If true, will capture the application's output to a log file.
			"changeWorkingDir": "c:\\some\\other\\path" // (Optional. Default: null) If informed, ProcessRunner will change the current directory to this one before running the program informed in the path property.
		}
	]
}

```


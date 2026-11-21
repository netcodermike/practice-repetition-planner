# Practice Repetition Planner

Allocate whole repetitions to practice exercises using weighted priorities and largest remainders.

![Sample run from Practice Repetition Planner](docs/demo.webp)

## Run

Install .NET 8 SDK and runtime. Run these commands from the repository root.

```sh
dotnet build --nologo
dotnet bin/Debug/net8.0/App.dll sample.json
```

Replace `sample.json` with a JSON file following the same structure. The app prints a JSON report to standard output.

Use nonnegative weights and a whole-number repetition budget. Equal remainders favor the earlier exercise in the input.

## Verify

Run `python check.py` with Python 3.11 or newer after the build step when applicable. The check verifies the bundled scenario and confirms that an invalid input returns an error.

# RSEQ (Robot Sequence)
Space Engineers Ingame Script to define and execute custom movement sequences

# Build
- Install and setup MDK
- Load scripts into MDK project
- Build

# Usage
This script defines a small custom language:
| Command | Function |
| ------------ | ---------------------------------- |
| `SEQ <name>` | Add a new sequence called `<name>` |
| `RUN <name>` | Run another sequence called `<name>` |
| `CALL <block> <action>` | Call a specific action on a block |
| `SET <block> <attribute> <value>` | Set a specific attribute |
| `ROTATE <block> <angle>` | Rotate a rotor or hinge to a specific angle, in degrees |
| `EXTEND <block> <distance>` | Extend or retract a piston to a specific distance (in meters) |

The list of sequences (see the example) must be written in the Custom Data section of the programmable block.
When the sequences are changed, the script must be recompiled (press `Recompile`).

# Example
```
SEQ Reset
ROTATE "Elevation Hinge" 0
ROTATE "Azimuth Hinge" 0
EXTEND Piston 0

SEQ "Move to left pickup"
ROTATE "Elevation Hinge" 90
ROTATE "Azimuth Hinge" 90
EXTEND Piston 1.5

SEQ "Move to right pickup"
ROTATE "Elevation Hinge" 90
ROTATE "Azimuth Hinge" -90
EXTEND Piston 1.5

SEQ "Pickup Tool"
CALL Connector Lock

SEQ "Release Left Tool"
RUN "Pickup Tool"
CALL "Landing Gear Left" Unlock

SEQ "Release Right Tool"
RUN "Pickup Tool"
CALL "Landing Gear Right" Unlock
```

To call these sequences, simply bind the "Run" option of the programmable block to your hotbar.
Which action is run is determined by the argument of the programmable block.
To run the `Move to right pickup` sequence, simply use the command line parameter `Move to right pickup`.
Quotes are only required when a name contains spaces.

# Current Issues
- There is no way to delay the execution, like wait for an extend to be finished before a hinge is rotated.
	- I might add this, not sure yet, probably a rather high priority though.
- The `SET` command does not work for colors.
	- Probably will not add this myself, I can barely see applications for this.

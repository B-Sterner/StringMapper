## Reason for public view
I was cleaning out my emails Thursday night (24 Apr 25). I saw that I had a rejection letter from a company that I had applied to about 8 months back. I had thought the company had just ghosted me, but their email was just not in my primary inbox. I prepared a proof-of-concept, in case I was interviewed. After seeing I was reached out to -- even though rejected -- I decided to share this with one of the personnel I spoke with.

As I never had an opportunity to discuss it, I am not sure if this was off target or potentially good.

## Background
Based on a conversation, I made a primitive proof-of-concept for how one could map a string to an object. In the conversation, it was stated that a string containing value pairs and single keywords would be received at intervals. These values/keywords would be parsed out and then used. 

If I recall correctly, there was a large group of if statements looking for specific tokens to parse out and make decisions based on that. It looked cumbersome.

## Proof of Concept
Note: I did not get specifics on the format of the string. And they did not have access to all C# language options – reduced set or old, I do not recall why.

With that in mind, I thought if I had a string that used unique separators for tokens, I could map that to an object. Then when a string was received, one could parse to the model and make decisions based on that. I new up the model each time, but this could be swapped out to a singleton and just updated each cycle.
## 5 parts are in the POC
- String Mapper: parses the string and maps to the model.  Two versions: one with Split and one with ReadOnlySpan.
- UserInterface: stores the input options and displays the results, also stores the sample data string.
- DataMessageModel: sample model with various rules
- BenchMark_StringMapper: for running the benchmarks
- Program: main loop
  
## Example String
The string is per line for easier human reading. I added some spacing issues to ensure it still worked. I did not add a rule for if qoutes matter, but one could add as needed.

            Notneeded1: Notneeded1;
            Notneeded2: Notneeded2;
            Notneeded3: Notneeded3;
            Notneeded4: Notneeded4;
            Notneeded5: Notneeded5;
            NotNeededKeyword1;
            
            Val1=no quote; 
            Val2='with quote';
            Val3=val3;
            Val4= val4;
            StringProp1='I am a longer string';
            StringProp2  =  another string;
            BoolVal1= false;
            BoolVal2= true;
            IntVal1=36;  
            DecVal=1.215942;
            Enabled;
            
            Notneeded11: Notneeded1;
            Notneeded12: Notneeded2;
            Notneeded13: Notneeded3;
            Notneeded14: Notneeded4;
            Notneeded15: Notneeded5;
            NotNeededKeyword11;
## Model
Based on the model in the demo, the middle portion of the data string is captured. The mapper will map to property names that have the same name as a key/keyword name. Hence, to capture another portion, add a property using the key/keyword name.

## performance
<img width="842" alt="image" src="https://github.com/user-attachments/assets/2165cf66-8470-419b-bd18-4ed7cd66c7d2" />

## results
<img width="334" alt="image" src="https://github.com/user-attachments/assets/6cbc27d6-1c83-4806-af71-1ffe461d4186" />

## Assumptions - There were many
I chose a semi-colon for a token separator and an equals sign for a kay value separator. These can be changed though. Also, one could implement regex if the tokens need more advanced logic.
The IsEnabled is set from two other properties in the model. One looks for “Enabled” keyword while the other the “Disbaled” keyword. Basically IsEnabled is used as a backing field for a keyword that indicates whether the device is enabled/disabled. This was added to account for when a single keyword indicated the status of something.

In slight contrast, DecVal uses a setter to round a decimal and then returns a backing field with the rounded value. This is pulled from a key value pair.

The other model properties are straight forward. 


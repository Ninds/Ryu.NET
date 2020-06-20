# GenerateTestData

Generates Test data in the form of .cs files and .txt file from the output of Remy Oudompheng's iterator over floating-point numbers which are hard to convert from/to decimal form. [fptest](https://github.com/remyoudompheng/fptest)


The input for this program can be generated from [fptest.py](https://github.com/remyoudompheng/fptest/blob/master/fptest.py)

`python parse64+ > parse64plus.txt` 

The output file is then the input for GenerateTestData

`dotnet GenerateTestData.dll parse64plus.txt  <Ryu.Net.s2d_data path>`



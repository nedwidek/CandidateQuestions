CandidateQuestions
==================

Simple C# program for Wake Forest Chamber of Commerce Candidate Forum. The Chamber's 
candidate's forum is held at the WF Town Hall Board Room. The board room provides a 
PC that drives the displays at each of the commissioner's seats, the moderator's 
podium, and a projection screen for the audience. So everyone sees the questions, 
which candidate's turn it is, and the count down timer.

This program provides an easy way to run a political candidate question forum if you 
have predetermined questions. The program provides a count down timer that will turn 
red in the last 10 seconds so the candidates get a warning to wrap up their answer. 
This is much better than cue cards or someone with a stop watch calling out 
"10 seconds." In 2012, the WF Chamber allowed one audience question and that was 
handled with a question that was entered in the configuration file as 
"Audience Question."

Each round asks one question and each candidate gets to answer. The questions are 
asked in a modified round-robin approach. The first candidate will be the first 
to answer the first question. The second candidate will be the first to answer the 
second question and the first candidate will the last to answer.

Round candidate progression exemplified:
Round 1: 1, 2, 3, 4, ...
Round 2: 2, 3, 4, ..., 1
Round 3: 3, 4, ..., 1, 2
Round 4: 4, ..., 1, 2, 3

The program is configured by three text files. Any line in these files that begins 
with a # indicates a comment and will be ignored by the program. Example:

# This is a comment.

title.txt - This file will contain the message you want displayed at the top of the 
program. E.G. - Wake Forest 2012 Candidate's Forum.

candidates.txt - This file contains the name of the candidates. Enter each 
candidate's name on a line by itself.

questions.txt - This file contains the questions, time limits, and font size. Each 
question is entered on a line by itself with the time limit and font size listed 
first. "60 36 How old are you?" will display the question in a 36 point font for 
1 minute. 

This program and the associated code are provided gratis under the BSD Revised 
license. This means that it is free of charge to use and may be freely modified to 
meet your needs.

Program History
---------------------------------------------------------------
I am a member of the Wake Forest Area Chamber's Government Affairs Committee 
(2011 - 2012). As a member I am involved in the planning of the Candidate's Forum 
and decided that this would be a good way to run the forum in 2011. The candidates 
and the Chamber staff were very pleased with how smoothly the forum went by using 
the software. Minor fixes were incorporated and used in 2012. There are already plans 
to reuse the program for 2013.

BSD Revised license
---------------------------------------------------------------

Copyright (c) 2011, 2012, Erik J Nedwidek / Hatteras Software
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are 
permitted provided that the following conditions are met:

Redistributions of source code must retain the above copyright notice, this list of 
conditions and the following disclaimer.

Redistributions in binary form must reproduce the above copyright notice, this list 
of conditions and the following disclaimer in the documentation and/or other 
materials provided with the distribution.

Neither the name of Erik J Nedwidek, Hatteras Software, or any other contributor may 
be used to endorse or promote products derived from this software without specific 
prior written permission.

THIS SOFTWARE IS PROVIDED BY Erik J Nedwidek, Hatteras Software, and any 
contributors "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT 
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR 
PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL Erik J Nedwidek, Hatteras Software, or any 
contributors BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS 
OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED 
AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT 
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 
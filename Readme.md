# About
JLTi CodeJam is a programming contest organized by Gopal Das for all the staffs in JLTi (Singapore & India). This repository consists both official questions, solutions and also my personal solutions.

Please refer to [https://gopalcdas.wordpress.com/category/jlti-code-jam/](https://gopalcdas.wordpress.com/category/jlti-code-jam/) for official site.

# Season 6 - Food as a Serivce (FaaS)

Threatened by the JLTi Weight Loss Competition where the participants are lining up in front of Salad shops, and the likes of me, who have entirely given up lunch (hopefully I can continue forever), food court shops who are selling oily, low-fibre and various other kinds of unhealthy food have come up with a novel idea.

Inspired from the software world, and more importantly, to attract the software people who sit in their chairs for long hours and are the primary victims of eating these junk, those food shops have chosen a name for this scheme – Food as a Service (FaaS), borrowed from the likes of SaaS, PaaS, IaaS – whatever that means, if that means anything at all.

Instead of paying on a daily basis, they are asking people to subscribe for food.

For example, without subscription, a set lunch would cost S$ 6, as usual, if you want to pay as you eat, just like as you are doing now. No strings attached.

However, if you subscribe for a week (5 meals, one meal one day, 5 consecutive days, not calendar week, can start at any day), instead of paying S$ 30, you can pay S$ 27.99 for five meals. Of course you have to eat from the same (chain of) shop.

And if you subscribe for a month (20 meals, one meal one day, 20 consecutive days, not calendar month, can start at any day) that they are vying for, you pay only S$ 99.99.

**Input**: 1, 2, 4, 5, 17, 18

**Output**: 36

**Explanation**: Input is a list of day numbers when you want to have a meal. The number can start at 1, and go up to any number.

A certain day number, say, 4, would not come more than once in the input, if it comes at all, assuming one can have only one lunch meal a day.

The above input says – you eat for 6 days. It makes no sense for you to go for a monthly subscription. Well, it also does not make sense to go for a weekly subscription. Paying daily basis for 6 days would be the best cost effective decision for you. You pay: S$ 36.

**Input**: 3, 4, 5, 6, 7, 17, 18

**Output**: 39.99

You subscribe for one week (first 5 days) and pay individually for the last 2 days. Your best decision cost you S$ 39.99.

**Input**: 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 14, 15, 16, 17, 19, 20, 21, 24

**Output**: 105.99

Here, a monthly subscription and S$ 6 for the last day would be the best deal for you.

**Task**: Given lunch calendar for some days (it can be 3 days, 10 days, 121 days or any number of days) as input, as explained above, I am planning to write a program that would output me the best price. Well, if I can find the best price, I also know what subscription plans etc. are. However, put that aside. Let’s find the best price, as shown and explained above.

Official question can be found [here](https://gopalcdas.wordpress.com/2017/08/05/faas/).   
Official solution can be found [here](https://gopalcdas.wordpress.com/2017/09/16/solution-faas/).  
My solution can be found [here](https://github.com/woo-chia-wei/jlti-code-jam/blob/master/CodeJam_06_FaaS/Program.cs) with below screenshot:

<p align="center"> 
  <img src="https://github.com/woo-chia-wei/jlti-code-jam/blob/master/Public/images/faas_01.png" 
       width="45%" height="45%">
</p>

# Season 7 - Team Lunch

Since our JLTi Mumbai colleagues started vising our Singapore office, we are having more team/project lunches. Usually, a number of them come together, and after a short while they also leave together. It is only few days before they leave that we start organizing team lunches. Suppose, there are three colleagues belonging to three different teams, then there would be three team lunches, one for each team.

However, not all members work for an exclusive team. For example, I create an impression as if I work for more than one team, and due to the good grace of those teams, I also get invited in their team lunches.

However, due to the rush of deliverables, that is the norm here, the team lunches are squeezed in the last few days, and at times, multiple team lunches fall on the same day, typically on the last day.

That is all fine and good for most. However, I have a big problem. If two team lunches fall on the same day, and I belong to both, I miss one for obvious reason. I skip lunch does not necessarily mean I skip free lunches.

Hence, I decided to write a small program that will take the team composition in certain way and output the minimum days required to schedule the lunches so that people working on multiple teams don’t miss out any.

Yes, I am not the only person but there are some other colleagues who also work across more than one team. Let us also assume that, for our 7 or 8 teams, it might be easy to calculate it manually. But when the number of team exceeds, say 100, then a program is a must.

**Input**:

1 2

**Output**: 2

**Explanation**:

Input 1 2 (1 and 2 separated by a space) means there are one or more members who belong to both team 1 and team 2.

Output 2 means, at least 2 days are required to arrange lunches for the teams. On day 1, one of the two teams can go for lunch. On the second day, the other can go.

How many teams are there? Well, there are at least 2. There can be more, but that is irrelevant. Suppose there are 4 more teams – team 3, team 4, team 5 and team 6, they can go either on first day or on second day. This is because, no member working in those 4 teams work for a second team. After all, the input says, only team 1 and team 2 have some common members.

**Input**:

1 2

2 3

**Output**: 2

We have some members common to team 1 and team 2. And there are some members, who are common to team 2 and team 3, as shown in the second line.

Each line in the input would indicate the presence of common members between two teams, where the two team numbers are separated by a space. There would be at least one line of input, meaning somebody would run this program only if there exist at least one member working for more than one team.

For the above input, we would still require at least two days to avoid any conflict. On one day team 1 and team 3 can go. Team 2 must go on a separate day.

**Input**:

1 2

2 3

1 3

**Output**: 3

Now we need 3 separate days. Team 1 cannot go on the same day as team 2 or team 3. This is because team 1 has members working for both team 2 and team 3. Similarly, team 2 cannot go for lunch on the same day as team 3 as they have common members. Hence, team 1, team 2 and team 3 – all need exclusive lunch days.

**Task**: Given a list of team pairs (like 1 2 is a team pair, as shown in input) sharing common members, we need to write a program, that would output the minimum number of days required to set aside for team lunches, so that nobody who work across multiple teams misses his/her share of team lunches.

Official question can be found [here](https://gopalcdas.wordpress.com/2017/09/09/team-lunch/).   
My solution can be found [here](https://github.com/woo-chia-wei/jlti-code-jam/blob/master/CodeJam_07_Team_Lunch/Program.cs) with below screenshot:

<p align="center"> 
  <img src="https://github.com/woo-chia-wei/jlti-code-jam/blob/master/Public/images/team_lunch_01.png" 
       width="45%" height="45%">
</p>

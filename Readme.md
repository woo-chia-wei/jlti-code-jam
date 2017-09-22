# About
This repository contains the codes I wrote for the JLTi CodeJam which is a programming contest open to all JLTi staffs.

# Season 7 - Food as a Serivce (FaaS)

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

Task: Given lunch calendar for some days (it can be 3 days, 10 days, 121 days or any number of days) as input, as explained above, I am planning to write a program that would output me the best price. Well, if I can find the best price, I also know what subscription plans etc. are. However, put that aside. Let’s find the best price, as shown and explained above.

Official question can be found [here](https://gopalcdas.wordpress.com/2017/08/05/faas/).   
Official solution can be found [here](https://gopalcdas.wordpress.com/2017/09/16/solution-faas/).  
My solution can be found [here](https://github.com/woo-chia-wei/jlti-code-jam/blob/master/CodeJam_07_FaaS/Program.cs).  
Console Screenshot:

<p align="center"> 
  <img src="https://github.com/woo-chia-wei/jlti-code-jam/blob/master/Public/images/faas_01.png" 
       width="70%" height="70%">
</p>

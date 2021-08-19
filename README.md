# Graduate Thesis Project: Newton's Apple (牛顿的苹果)
My graduate thesis project (Unity 2D) is an apple catching game with two versions of simulated dynamic difficulty adjustment (DDA). Players are going to play as the childhood Newton and accumulate the target scores by grabbing adequately falling apples within the limited time. However, some apples should not be grabbed. The falling apples are primarily separated as either good apples or bad apples; the good apple has the corresponding worth points, but the bad apples can reduce players' cumulative scores until their scores are 0. The values of players' target cumulative scores are 100 points, but players should only reach and keep the goal scores within 5 minutes (300 seconds); after 5 minutes, players' final scores and the times of reaching the highest points will be recorded. During the process, if players' scores have already reached 100 points, the next steps are keeping 100 points until the game is over; in this case, the game system will start counting the times of reaching the goal score, and the game will provide the corresponding popup text to remind players. 

In the depth version, except apples, no new elements will be introduced, but the falling speeds of bad apples will be gradually changed once players' scores vary. 

However, in the complexity version, all items' falling speeds are equal to each other, but new elements will be introduced when players' scores go up, there are 3 rewards and 5 punishments:

Rewards:
 1. Rocket card (moving speed +50%)
 2. Heart card (next round: 5 red apples)
 3. Bit coin card (next round: 5 golden apples)

Punishments:
 1. Broom card (score -50%)
 2. Slow meter card (moving speed -50%)
 3. Rotten apple card (next round: 5 rotten apples)
 4. Rotten toxic apple card  (next round: 3 rotten apples + 2 toxic apples)
 5. Toxic apple card (next round: 5 toxic apples)

//Redis Value Types : Key value must be max 512mb

//String

/*
 * set keyname "value"
 * get keyname
 * incr keyname : Only numbers for +1
 * incrby keyname 10 : for +10
 * decr keyname : Only numbers for -1
 * decrby keyname 10 : for -10
 * append keyname "value" :  Add New Value On Cuurent Key
 */
 
//List

/*
 * lpush listname "item1" : from first index
 * rpush listname "item2" : from last index
 * lrange listname index1 index2 : list by index range
 * lpop listname : remove first item from list
 * rpop listname : remove last item from list
 * lindex listname index : get list item by index number
 */
 
//Set

/*
 * must be Uniqe list items in list
 * adding random place in list
 * sadd keyname "value"
 * smembers keyname : list set members
 * srem keyname "value" : remove an item from set list
 */
 
//Sorted Set

/*
 * zadd keyname index "value" : add new item sorted set list item on list
 * zrange keyvalue firstindex lastindex withscores : get all list
 * zrem keyname "value" : remove an item from sorted set list
 */
 
//Hash

/*
 * hmset hashname keyname keyvalue : create a new hash and add new keyname, value into the hash data
 * hget hashname keyname : get hash key value by keyname
 * hgetall hashname : get all list
 */

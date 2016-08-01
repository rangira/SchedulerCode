Trie = function() {
    this.words = 0;
    this.prefixes = 0;
    this.children = [];
};

Trie.prototype = {


    insert: function (str, pos) {

        if (str.length == 0) {
            return;
        }

        var T = this, k;

        if (pos === undefined) {
            pos = 0;
        }

        if (pos === str.length) {
            T.words++;
            return;
        }

        T.prefixes++;
        k = str[pos];
        if (T.children[k] === undefined) {
            T.children[k] = new Trie();
        }

        var child = T.children[k];
        child.insert(str, pos + 1);


    },

    find: function (pattern) {
        var T = this, k, child, ret = [];

        if (pattern===undefined) {
            pattern= "";
        }
        if (T===undefined) {
            return [];
        }
        if (T.words > 0) {
            ret.push(pattern);
        }
        for (k in T.children) {
            child = T.children[k];
            ret = ret.concat(child.find(pattern + k));
        }
        
        return ret;

    },
    autoComplete: function (str, pos) {
        if (str.length == 0) {
            return [];
        }

        var T = this,k,child;

        if (pos === undefined) {
            pos = 0;
        }
        k = str[pos];
        child = T.children[k];
        if (child === undefined) { //node doesn't exist
            return [];
        }
        if (pos === str.length - 1) {
            return child.find(str);
        }
        return child.autoComplete(str, pos + 1);
    }
};


var arrayStates = [
        "Alabama",
        "Alaska",
        "Arizona",
        "Arkansas",
        "California",
        "Colorado",
        "Connecticut",
        "Delaware",
        "Florida",
        "Georgia",
        "Hawaii",
        "Idaho",
        "Illinois",
        "Indiana",
        "Iowa",
        "Kansas",
        "Kentucky",
        "Louisiana",
        "Maine",
        "Maryland",
        "Massachusetts",
        "Michigan",
        "Minnesota",
        "Mississippi",
        "Missouri",
        "Montana",
        "Nebraska",
        "Nevada",
        "New Hampshire",
        "New Jersey",
        "New Mexico",
        "New York",
        "North Dakota",
        "North Carolina",
        "Ohio",
        "Oklahoma",
        "Oregon",
        "Pennsylvania",
        "Rhode Island",
        "South Carolina",
        "South Dakota",
        "Tennessee",
        "Texas",
        "Utah",
        "Vermont",
        "Virginia",
        "Washington",
        "West Virginia",
        "Wisconsin",
        "Wyoming"
];







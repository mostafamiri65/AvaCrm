// npm package: typeahead.js
// github link: https://github.com/twitter/typeahead.js

$(function() {
    'use strict'
  
    var substringMatcher = function(strs) {
      return function findMatches(q, cb) {
        var matches, substringRegex;
  
        // an array that will be populated with substring matches
        matches = [];
  
        // regex used to determine if a string contains the substring `q`
        var substrRegex = new RegExp(q, 'i');
  
        // iterate through the pool of strings and for any string that
        // contains the substring `q`, add it to the `matches` array
        for (var i = 0; i < strs.length; i++) {
          if (substrRegex.test(strs[i])) {
            matches.push(strs[i]);
          }
        }
  
        cb(matches);
      };
    };
  
    var states = ['Ł…Ų±Ś©Ų²Ū', 'Ų§Ų±ŲÆŲØŪŁ„', 'Ų¢Ų°Ų±ŲØŲ§ŪŲ¬Ų§Ł† ŲŗŲ±ŲØŪ', 'Ų§ŲµŁŁ‡Ų§Ł†', 'Ų®ŁŲ²Ų³ŲŖŲ§Ł†',
      'Ų§ŪŁ„Ų§Ł…', 'Ų®Ų±Ų§Ų³Ų§Ł† Ų´Ł…Ų§Ł„Ū', 'Ł‡Ų±Ł…Ų²ŚÆŲ§Ł†', 'ŲØŁŲ´Ł‡Ų±', 'Ų®Ų±Ų§Ų³Ų§Ł† Ų¬Ł†ŁŲØŪ', 'Ų¢Ų°Ų±ŲØŲ§ŪŲ¬Ų§Ł† Ų´Ų±Ł‚Ū',
      'ŲŖŁ‡Ų±Ų§Ł†', 'Ł„Ų±Ų³ŲŖŲ§Ł†', 'ŚÆŪŁ„Ų§Ł†', 'Ų³ŪŲ³ŲŖŲ§Ł† Ł ŲØŁ„ŁŚ†Ų³ŲŖŲ§Ł†', 'Ų²Ł†Ų¬Ų§Ł†', 'Ł…Ų§Ų²Ł†ŲÆŲ±Ų§Ł†', 'Ų³Ł…Ł†Ų§Ł†',
      'Ś©Ų±ŲÆŲ³ŲŖŲ§Ł†', 'Ś†Ł‡Ų§Ų±Ł…Ų­Ų§Ł„ Ł ŲØŲ®ŲŖŪŲ§Ų±Ū', 'ŁŲ§Ų±Ų³', 'Ł‚Ų²ŁŪŁ†', 'Ł‚Ł…',
      'Ų§Ł„ŲØŲ±Ų²', 'Ś©Ų±Ł…Ų§Ł†', 'Ś©Ų±Ł…Ų§Ł†Ų´Ų§Ł‡', 'ŚÆŁ„Ų³ŲŖŲ§Ł†', 'Ų®Ų±Ų§Ų³Ų§Ł† Ų±Ų¶ŁŪ', 'Ł‡Ł…ŲÆŲ§Ł†',
      'Ś†Ł‡Ų§Ų±Ł…Ų­Ų§Ł„ Ł ŲØŲ®ŲŖŪŲ§Ų±Ū', 'Ś©Ł‡Ś©ŪŁ„ŁŪŁ‡ Ł ŲØŁŪŲ±Ų§Ų­Ł…ŲÆ', 'ŪŲ²ŲÆ'
    ];
  
    $('#the-basics .typeahead').typeahead({
      hint: true,
      highlight: true,
      minLength: 1
    }, {
      name: 'states',
      source: substringMatcher(states)
    });
    // constructs the suggestion engine
    var states = new Bloodhound({
      datumTokenizer: Bloodhound.tokenizers.whitespace,
      queryTokenizer: Bloodhound.tokenizers.whitespace,
      // `states` is an array of state names defined in "The Basics"
      local: states
    });
  
    $('#bloodhound .typeahead').typeahead({
      hint: true,
      highlight: true,
      minLength: 1
    }, {
      name: 'states',
      source: states
    });
  
  });
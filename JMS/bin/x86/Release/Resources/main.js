var $output;
var _inited = false;
var _locked = false;
var _buffer = [];
var _obuffer = [];
var _ibuffer = [];
var _cwd = "/";
var _prompt = function() { return _cwd + " $ "; };
var _history = [];
var _hindex = -1;
var _lhindex = -1;

/////////////////////////////////////////////////////////////////
// UTILS
/////////////////////////////////////////////////////////////////

function setSelectionRange(input, selectionStart, selectionEnd) {
    if (input.setSelectionRange) {
        input.focus();
        input.setSelectionRange(selectionStart, selectionEnd);
    }
    else if (input.createTextRange) {
        var range = input.createTextRange();
        range.collapse(true);
        range.moveEnd('character', selectionEnd);
        range.moveStart('character', selectionStart);
        range.select();
    }
}

function padRight(str, l, c) {
    return str+Array(l-str.length+1).join(c||" ")
}

function padCenter(str, width, padding) {
    var _repeat = function(s, num) {
        for( var i = 0, buf = ""; i < num; i++ ) buf += s;
        return buf;
    };

    padding = (padding || ' ').substr( 0, 1 );
    if ( str.length < width ) {
        var len     = width - str.length;
        var remain  = ( len % 2 === 0 ) ? "" : padding;
        var pads    = _repeat(padding, parseInt(len / 2));
        return pads + str + pads + remain;
    }

    return str;
}

window.requestAnimFrame = (function(){
    return  window.requestAnimationFrame       ||
    window.webkitRequestAnimationFrame ||
    window.mozRequestAnimationFrame    ||
    function( callback ){
        window.setTimeout(callback, 1000 / 60);
    };
})();

/////////////////////////////////////////////////////////////////
// SHELL
/////////////////////////////////////////////////////////////////

(function animloop(){
    requestAnimFrame(animloop);

    if ( _obuffer.length ) {
        $output.value += _obuffer.shift();
        _locked = true;

        update();
    } else {
        if ( _ibuffer.length ) {
        $output.value += _ibuffer.shift();

        update();
    }

    _locked = false;
    _inited = true;
    }
})();

function print(input, lp) {
    update();
    _obuffer = _obuffer.concat(lp ? [input] : input.split(''));
}

function update() {
    $output.focus();
    var l = $output.value.length;
    setSelectionRange($output, l, l);
    $output.scrollTop = $output.scrollHeight;
}

function clear() {
    $output.value = '';
    _ibuffer = [];
    _obuffer = [];
    print("");
}

window.onload = function() {
    $output = document.getElementById("output");
    $output.contentEditable = true;
    $output.spellcheck = false;
    $output.value = '';

    $output.onblur = function() {
        update();
    };

    window.onfocus = function() {
        update();
    };

    print("Initializing RetroChat 3000 v1.0 ....................................................\n");

    //print("------------------------------------------------------------------------------------------------------------------");
    print("                  @@@  @@@  @@@  @@@@@@@@  @@@        @@@@@@@   @@@@@@   @@@@@@@@@@   @@@@@@@@                  \n", true);
    print("                  @@@  @@@  @@@  @@@@@@@@  @@@       @@@@@@@@  @@@@@@@@  @@@@@@@@@@@  @@@@@@@@                  \n", true);
    print("                  @@!  @@!  @@!  @@!       @@!       !@@       @@!  @@@  @@! @@! @@!  @@!                       \n", true);
    print("                  !@!  !@!  !@!  !@!       !@!       !@!       !@!  @!@  !@! !@! !@!  !@!                       \n", true);
    print("                  @!!  !!@  @!@  @!!!:!    @!!       !@!       @!@  !@!  @!! !!@ @!@  @!!!:!                    \n", true);
    print("                  !@!  !!!  !@!  !!!!!:    !!!       !!!       !@!  !!!  !@!   ! !@!  !!!!!:                    \n", true);
    print("                  !!:  !!:  !!:  !!:       !!:       :!!       !!:  !!!  !!:     !!:  !!:                       \n", true);
    print("                  :!:  :!:  :!:  :!:        :!:      :!:       :!:  !:!  :!:     :!:  :!:                       \n", true);
    print("                   :::: :: :::    :: ::::   :: ::::   ::: :::  ::::: ::  :::     ::    :: ::::                  \n", true);
    print("                    :: :  : :    : :: ::   : :: : :   :: :: :   : :  :    :      :    : :: ::                   \n", true);
    print("\n\n\n", true);

    print(padCenter("All graphics are created using CSS, no static files or images\n", 113), true);

    print("\n\n\n\n\n", true);
    print("Type '!howto' to learn how to play.\n", true);
};
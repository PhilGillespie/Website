var PageTransitions = (function () {

    var $main = $('#pt-main'),
		$pages = $main.children('div.pt-page'),
        //$links = $("linksParent").children('div.btn');
		$iterate = $('#iterateEffects'),
		animcursor = 1,
		pagesCount = $pages.length,
		current = 'home-link',
		isAnimating = false,
		endCurrPage = false,
		endNextPage = false,
        outClass = 'pt-page-rotateLeftSideFirst',
        inClass = 'pt-page-moveFromLeft pt-page-delay200 pt-page-ontop',
		animEndEventNames = {
		    'WebkitAnimation': 'webkitAnimationEnd',
		    'OAnimation': 'oAnimationEnd',
		    'msAnimation': 'MSAnimationEnd',
		    'animation': 'animationend'
		},
        frameCount = 1,
        start = null,
		// animation end event name
		animEndEventName = animEndEventNames[Modernizr.prefixed('animation')],
		// support css animations
		support = Modernizr.cssanimations;

    //var requestAnimationFrame = window.mozRequestAnimationFrame ||
    //  window.webkitRequestAnimationFrame ||
    //  window.msRequestAnimationFrame ||
    //  window.requestAnimationFrame;
    //  window.requestAnimationFrame = requestAnimationFrame;

    function init() {

        $pages.each(function () {
            var $page = $(this);
            $page.data('originalClassList', $page.attr('class'));
        });

        $pages.eq(current).addClass('pt-page-current');

        $('.navbar-link').click(function () {
            var id = $(this).attr('id');
            if (id == current) {
                return false;
            }
            console.log(id);
            $('.navbar-link.' + current + '.current').removeClass('current');
            $('.navbar-link.' + id).addClass('current');
            nextPage(id);
        });
    }

    function nextPage(pageId) {
        var $nextPage = $main.children('.' + pageId).addClass('pt-page-current');
        if (isAnimating) {
            return false;
        }

        isAnimating = true;

        var $currPage = $main.children('.' + current);
        current = pageId;

        $currPage.addClass(outClass).on(animEndEventName, function () {
            $currPage.off(animEndEventName);
            endCurrPage = true;
            if (endNextPage) {
                onEndAnimation($currPage, $nextPage);
            }
        });

        $nextPage.addClass(inClass).on(animEndEventName, function () {
            $nextPage.off(animEndEventName);
            endNextPage = true;
            if (endCurrPage) {
                onEndAnimation($currPage, $nextPage);
            }
        });

        if (!support) {
            onEndAnimation($currPage, $nextPage);
        }
    }

    function onEndAnimation($outpage, $inpage) {
        endCurrPage = false;
        endNextPage = false;
        resetPage($outpage, $inpage);
        isAnimating = false;
    }

    function resetPage($outpage, $inpage) {
        $outpage.removeClass('pt-page-current').removeClass(outClass);
        $inpage.removeClass(inClass);
    }

    function rotateCube(timestamp) {
        if (!start) start = timestamp;
        var progress = timestamp - start;
        frameCount++;
        $(".cube").css("transform", "rotateX(-20deg) rotateY(" + -frameCount + "deg)");
        requestAnimationFrame(rotateCube);
    }

    requestAnimationFrame(rotateCube);

    init();

    return {
        init: init,
        nextPage: nextPage,
    };

})();
function findAppFrame() {
	if ('undefined' != typeof(self.parent.data)) {
		return self.parent;
	} else if ('undefined' != typeof(self.parent.parent.data)) {
		return self.parent.parent;
	} else if ('undefined' != typeof(self.parent.parent.parent.data)) {
		return self.parent.parent.parent;
	} else {
		return window.top;
	}
}
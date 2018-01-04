class CopError(Exception):

    _message = None

    def __init__(self, message):
        if message is None:
            self._message = '<undefined>'
        else:
            self._message = message

    def __str__(self):
        return repr(self._message)

import {useEffect, useRef} from "react";

export default function useInterval(callback: () => void, delay: number) {
    useIntervalInMs(callback, delay * 1000)
}


export function useIntervalInMs(callback: () => void, delay: number) {
    const savedCallback = useRef(() => {
    })

    useEffect(() => {
        savedCallback.current = callback;
    }, [callback])

    useEffect(() => {
        function tick() {
            savedCallback.current()
        }

        if (delay !== null) {
            const id = setInterval(tick, delay)
            return () => {
                clearInterval(id)
            }
        }
    }, [callback, delay])
}

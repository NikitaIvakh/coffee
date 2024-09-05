import React, { useRef } from 'react'
import { BrowserRouter as Router, Route, Routes, useLocation } from 'react-router-dom'
import { CSSTransition, SwitchTransition } from 'react-transition-group'
import ControlDetails from './features/details/ControlDetails'
import Main from './pages/Main'
import NotFound from './pages/NotFound'
import OurCoffee from './pages/OurCoffee'
import './styles/app.scss'

const AnimatedRoutes = () => {
	const location = useLocation()
	const nodeRef = useRef(null)
	const duration = 300
	
	return (
		<SwitchTransition>
			<CSSTransition
				key={location.pathname}
				nodeRef={nodeRef}
				timeout={duration}
				classNames='my-node'
				unmountOnExit
			>
				<div ref={nodeRef}>
					<Routes location={location}>
						<Route path='/' element={<Main />} />
						<Route path='/OurCoffee' element={<OurCoffee />} />
						<Route path='/OurCoffee/:id' element={<ControlDetails />} />
						<Route path='*' element={<NotFound />} />
					</Routes>
				</div>
			</CSSTransition>
		</SwitchTransition>
	)
}

function App() {
	return (
		<Router>
			<AnimatedRoutes />
		</Router>
	)
}

export default App

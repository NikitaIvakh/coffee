import { useRef } from 'react'
import { BrowserRouter as Router, Route, Routes, useLocation } from 'react-router-dom'
import { CSSTransition, SwitchTransition } from 'react-transition-group'
import './styles/app.scss'
import { AdminPanel, ControlsOurCoffee, ControlsOurPleasure, Main, NotFound, OurCoffee, Pleasure } from './pages'

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
						<Route path='/OurCoffee/:id' element={<ControlsOurCoffee />} />
						<Route path='/Pleasure' element={<Pleasure />} />
						<Route path='/Pleasure/:id' element={<ControlsOurPleasure />} />
						<Route path='/AdminPanel' element={<AdminPanel />} />
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

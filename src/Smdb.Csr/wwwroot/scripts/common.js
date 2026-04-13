export const API_BASE = 'http://localhost:8080/api/v1';
export const $ = (sel, el = document) => el.querySelector(sel);
export const $$ = (sel, el = document) => Array.from(el.querySelectorAll(sel));
export const getQueryParam = (k) => new URLSearchParams(location.search).get(k);

function jsonHeaders() {
	return { 'Content-Type': 'application/json', 'Accept': 'application/json' };
}

export async function apiFetch(path, opts = {}) {
	const url = path.startsWith('http') ? path : `${API_BASE}${path}`;
	const init = {
		...opts,
		headers: { ...(opts.headers || {}), ...jsonHeaders() }
	};
	const res = await fetch(url, init);
	const text = await res.text();
	let payload = null;
	try { payload = text ? JSON.parse(text) : null; } catch { payload = text; }
	if (!res.ok) {
		const msg = (payload && (payload.message || payload.error)) ||
			`${res.status} ${res.statusText}`;
		const err = new Error(msg);
		err.status = res.status;
		err.payload = payload;
		throw err;
	}
	return payload;
}

export function renderStatus(el, type, message) {
	if (!el) return;
	el.className = `status ${type}`;
	el.textContent = message;
}

export function clearChildren(el) {
	el.replaceChildren();
}

export function setupPagination(payload, page, size) {
	const pageNumEl = $('#page-num');
	if (pageNumEl) pageNumEl.textContent = `Page ${page}`;
	const firstPage = page <= 1;
	const totalPages = payload?.meta?.totalPages ?? page;
	const lastPage = page >= totalPages;
	const firstBtn = $('#first');
	const prevBtn = $('#prev');
	const nextBtn = $('#next');
	const lastBtn = $('#last');
	if (!firstBtn) return;
	firstBtn.href = `?page=1&size=${size}`;
	prevBtn.href = `?page=${page - 1}&size=${size}`;
	nextBtn.href = `?page=${page + 1}&size=${size}`;
	lastBtn.href = `?page=${totalPages}&size=${size}`;
	firstBtn.classList.toggle('disabled', firstPage);
	prevBtn.classList.toggle('disabled', firstPage);
	nextBtn.classList.toggle('disabled', lastPage);
	lastBtn.classList.toggle('disabled', lastPage);
	firstBtn.setAttribute('onclick', `return ${!firstPage};`);
	prevBtn.setAttribute('onclick', `return ${!firstPage};`);
	nextBtn.setAttribute('onclick', `return ${!lastPage};`);
	lastBtn.setAttribute('onclick', `return ${!lastPage};`);
}

export function setupPageSizeSelect(size) {
	const sizeSelect = document.getElementById('page-size');
	if (!sizeSelect) return;
	const pageSizes = [3, 6, 9, 12, 15];
	for (const s of pageSizes) {
		const opt = document.createElement('option');
		opt.value = s;
		opt.textContent = String(s);
		opt.selected = (size == s);
		sizeSelect.appendChild(opt);
	}
	sizeSelect.addEventListener('change', () => {
		const params = new URLSearchParams(window.location.search);
		params.set('page', 1);
		params.set('size', sizeSelect.value);
		localStorage.setItem('page', 1);
		localStorage.setItem('size', sizeSelect.value);
		window.location.href = `${window.location.pathname}?${params.toString()}`;
	});
}

export function captureMovieForm(form) {
	return {
		title: form.title.value.trim(),
		year: Number(form.year.value),
		description: form.description.value.trim(),
	};
}

export function captureActorForm(form) {
	return {
		name: form.name.value.trim(),
		age: Number(form.age.value),
	};
}

export function captureUserForm(form) {
	return {
		name: form.name.value.trim(),
		email: form.email.value.trim(),
	};
}

export function captureActorMovieForm(form) {
	return {
		actorId: Number(form.actorId.value),
		movieId: Number(form.movieId.value),
	};
}
